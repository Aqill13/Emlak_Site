using BusinessLayer.Abstract;
using EmlakUI.Areas.Admin.Models;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmlakUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<UserAdmin> _userManager;
        private readonly SignInManager<UserAdmin> _signInManager;

        public AdminController(UserManager<UserAdmin> userManager, SignInManager<UserAdmin> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
            return RedirectToAction(nameof(Login), "Admin");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
