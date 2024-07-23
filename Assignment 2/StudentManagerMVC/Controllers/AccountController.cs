using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentManagerMVC.Areas.Identity.Data;
using StudentManagerMVC.Models;
using static StudentManagerMVC.Models.ViewModel;
namespace StudentManagerMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<StudentManagerMVCUser> _userManager;
        private readonly SignInManager<StudentManagerMVCUser> _signInManager;
        private readonly Data.AuthenticationContext _context;

        public AccountController(
            UserManager<StudentManagerMVCUser> userManager,
            SignInManager<StudentManagerMVCUser> signInManager,
            Data.AuthenticationContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new StudentManagerMVCUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Save the user to your custom table
                    var newUser = new RegisteredUser
                    {
                        UserId = user.Id,
                        Email = user.Email,
                        DateRegistered = DateTime.Now
                    };
                    _context.registeredUsers.Add(newUser);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("RegisterSuccess");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }


        // GET: /Account/RegisterSuccess
        [HttpGet]
        public IActionResult RegisterSuccess()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("LoginSuccess");
                }
                if (result.IsLockedOut)
                {
                    return View("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        public IActionResult LoginSuccess()
        {
            return View();
        }

    }

}
