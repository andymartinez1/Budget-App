using BudgetApp.Models;
using BudgetApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BudgetApp.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly SignInManager<BudgetUser> _signInManager;
    private readonly UserManager<BudgetUser> _userManager;

    public AccountController(
        SignInManager<BudgetUser> signInManager,
        UserManager<BudgetUser> userManager,
        ILogger<AccountController> logger
    )
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _logger = logger;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginVm)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(
                loginVm.Email,
                loginVm.Password,
                loginVm.RememberMe,
                false
            );

            if (result.Succeeded)
            {
                _logger.LogInformation("User {User} has logged in.", loginVm.Email);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Email or password is incorrect.");
        }

        return View(loginVm);
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerVm)
    {
        if (ModelState.IsValid)
        {
            var user = new BudgetUser
            {
                FullName = registerVm.Name,
                Email = registerVm.Email,
                UserName = registerVm.Email,
            };

            var result = await _userManager.CreateAsync(user, registerVm.Password);

            if (result.Succeeded)
                return RedirectToAction("Login", "Account");

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

        return View(registerVm);
    }

    public IActionResult VerifyEmail()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> VerifyEmail(VerifyEmailViewModel verifyEmailVm)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(verifyEmailVm.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Something went wrong.");
                return View(verifyEmailVm);
            }

            return RedirectToAction("ChangePassword", "Account", new { username = user.UserName });
        }

        return View(verifyEmailVm);
    }

    public IActionResult ChangePassword(string username)
    {
        if (string.IsNullOrEmpty(username))
            return RedirectToAction("VerifyEmail", "Account");

        return View(new ChangePasswordViewModel { Email = username });
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordVm)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(changePasswordVm.Email);
            if (user != null)
            {
                var result = await _userManager.RemovePasswordAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddPasswordAsync(
                        user,
                        changePasswordVm.NewPassword
                    );
                    return RedirectToAction("Login", "Account");
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
                return View(changePasswordVm);
            }

            ModelState.AddModelError("", "Email not found");
            return View(changePasswordVm);
        }

        ModelState.AddModelError("", "Something went wrong.");
        return View(changePasswordVm);
    }

    public async Task<IActionResult> Logout()
    {
        var username = User?.Identity?.Name ?? "Unknown";

        await _signInManager.SignOutAsync();
        _logger.LogInformation("User {User} has logged out.", username);

        return RedirectToAction("Index", "Home");
    }
}
