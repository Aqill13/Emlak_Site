using BusinessLayer.Abstract;
using EmlakUI.Areas.User.Models;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmlakUI.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    public class UserController : Controller
    {
        private readonly UserManager<UserAdmin> _userManager;
        private readonly SignInManager<UserAdmin> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService _userService;

        public UserController(UserManager<UserAdmin> userManager, SignInManager<UserAdmin> signInManager,
            RoleManager<IdentityRole> roleManager, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        // -----  Login process  ------

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && user.EmailConfirmed)
            {
                var login = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (login.Succeeded)
                {
                    return RedirectToAction("Index", "User");
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

        // ----- Register process -----

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Random random = new Random();
            int code = random.Next(100000, 999999);

            UserAdmin newUser = new UserAdmin
            {
                Email = model.Email,
                FullName = model.Firstname + " " + model.Lastname,
                UserName = model.Username,
                ControlCode = code,
            };
            var createUser = await _userManager.CreateAsync(newUser, model.Password);

            var userRole = await _roleManager.FindByNameAsync("User");
            if (userRole == null)
            {
                userRole = new IdentityRole
                {
                    Name = "User"
                };
                await _roleManager.CreateAsync(userRole);
            }

            if (createUser.Succeeded)
            {
                var addToRole = await _userManager.AddToRoleAsync(newUser, userRole.Name);

                if (addToRole.Succeeded)
                {
                    TempData["code"] = code;
                    TempData["mail"] = model.Email;
                    return RedirectToAction(nameof(ConfirmMail), "User");
                }
            }
            else
            {
                foreach (var item in createUser.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }

        // ----- ConfirmMail -----

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ConfirmMail()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmMail(ConfirmMail confirmMail)
        {
            if (!ModelState.IsValid)
            {
                TempData["mail"] = confirmMail.Email;
                return View(confirmMail);
            }

            var user = await _userManager.FindByEmailAsync(confirmMail.Email);
            if (user != null && user.ControlCode == confirmMail.Code)
            {
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
                return RedirectToAction(nameof(Login), "User");
            }
            else
            {
                TempData["mail"] = confirmMail.Email;
                ModelState.AddModelError("", "Please check your email");
                return View(confirmMail);
            }
        }

        // ----- LogOut process ----- 

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login), "User");
        }
    }
}
