using Microsoft.AspNetCore.Mvc;

namespace BudgetApp.Controllers;

public class AccountController : Controller
{
    // GET
    public IActionResult Login()
    {
        return View();
    }
}
