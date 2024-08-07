using BusinessLayer.Abstract;
using EmlakUI.Areas.Admin.Models;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmlakUI.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class AdminController : Controller
    {
        private readonly UserManager<UserAdmin> _userManager;
        private readonly SignInManager<UserAdmin> _signInManager;
        private readonly IUserService _userService;

        public AdminController(UserManager<UserAdmin> userManager,
            IUserService userService, SignInManager<UserAdmin> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var admin = await _userManager.FindByNameAsync(model.Username);
            if (admin != null)
            {
                var login = await _signInManager.PasswordSignInAsync(admin, model.Password, model.RememberMe, false);
                if (login.Succeeded)
                {
                    if (await _userManager.IsInRoleAsync(admin, "admin"))
                    {
                        return RedirectToAction("Index", "Advert", new { area = "Admin" });
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Username or password is incorrect");
                }
            }
            else
            {
                ModelState.AddModelError("", "Username or password is incorrect");
            }
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login), "Admin");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }

    /*public class AdminController : Controller
    {
        private readonly UserManager<UserAdmin> _userManager;
        private readonly SignInManager<UserAdmin> _signInManager;
        private readonly IUserService _userService;

        public AdminController(UserManager<UserAdmin> userManager,
            IUserService userService, SignInManager<UserAdmin> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var admin = await _userManager.FindByNameAsync(model.Username);

                if (admin != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(admin, model.Password, model.RememberMe, false);

                    if (result.Succeeded)
                    {
                        var isAdmin = await _userManager.IsInRoleAsync(admin, "admin");
                        if (isAdmin)
                        {
                            TempData["adminId"] = _userService.GetUserId();
                            await HttpContext.SignInAsync("AdminScheme",
                                new ClaimsPrincipal(new ClaimsIdentity(new[]
                                {
                                new Claim(ClaimTypes.Name, admin.UserName),
                                new Claim(ClaimTypes.Role, "admin")
                                }, "AdminScheme")));
                            return RedirectToAction("Index", "Advert");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Unauthorized access.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Username or Password is incorrect");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is incorrect");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync("AdminScheme");
            return RedirectToAction(nameof(Login), "Admin");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }*/
}   
