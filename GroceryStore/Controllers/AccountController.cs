using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GroceryStore.Models.ViewModels;

namespace GroceryStore.Controllers
{
    public class AccountController : Controller
    {

        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

        }

        #region Account
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            ApplicationUser user = new ApplicationUser
            {
                Email = model.Email,
              
                UserName = model.UserName
            };

            var res = await _userManager.CreateAsync(user, model.Password);
            if (res.Succeeded)

            {
                return RedirectToAction("Login");
            }
            foreach (var err in res.Errors)
            {
                ModelState.AddModelError("", err.Description);
            }
            return View(model);



        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    //return RedirectToAction("Index", "Home", new { area = "Dashboard" });
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid User or password");
                return View(model);

            }
            return View(model);
        }

      
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        [HttpGet]
        public IActionResult RolesList()
        {
            return View(_roleManager.Roles);
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

    }
}
