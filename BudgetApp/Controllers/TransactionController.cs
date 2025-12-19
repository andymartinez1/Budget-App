using BudgetApp.Models;
using BudgetApp.Models.ViewModels;
using BudgetApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetApp.Controllers;

public class TransactionController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly ITransactionService _transactionService;

    public TransactionController(
        ITransactionService transactionService,
        ICategoryService categoryService
    )
    {
        _transactionService = transactionService;
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string filterCategory, string searchName)
    {
        var transactions = await _transactionService.GetAllTransactionsAsync();
        var categories = await _categoryService.GetAllCategoriesAsync();

        var transactionVm = new TransactionCategoryViewModel
        {
            Categories = GetCategorySelectList(categories),
            FilterCategory = filterCategory,
            Transactions = transactions,
            SearchName = searchName,
        };

        return View(transactionVm);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var transactionDetailsVm = await _transactionService.GetTransactionDetailsAsync(id);

        return PartialView("_DetailsModalPartial", transactionDetailsVm);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();

        var transactionEditVm = new TransactionViewModel
        {
            Categories = GetCategorySelectList(categories),
        };

        return PartialView("_CreateModalPartial", transactionEditVm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Name,Date,Amount,Category")] TransactionViewModel transaction
    )
    {
        var transactionCreateVm = await _transactionService.AddTransactionAsync(transaction);

        return PartialView("_CreateModalPartial", transactionCreateVm);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var transaction = await _transactionService.GetTransactionByIdAsync(id);
        var categories = await _categoryService.GetAllCategoriesAsync();
        var category = await _categoryService.GetCategoryByIdAsync(transaction.CategoryId);

        var transactionEditVm = new TransactionViewModel
        {
            Amount = transaction.Amount,
            Date = transaction.Date,
            Name = transaction.Name,
            Id = transaction.TransactionId,
            Category = category.Type,
            CategoryId = transaction.CategoryId,
            Categories = GetCategorySelectList(categories),
        };

        return PartialView("_EditModalPartial", transactionEditVm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        [Bind("Id,Name,Date,Amount,Category,Categories")] TransactionViewModel transactionVm
    )
    {
        if (ModelState.IsValid)
        {
            var category = await _categoryService.GetCategoryByIdAsync(transactionVm.CategoryId);
            if (category is null)
                return NotFound();

            transactionVm.CategoryViewModel = new CategoryViewModel();

            await _transactionService.UpdateTransactionAsync(transactionVm.Id);

            return Json(new { success = true });
        }

        return PartialView("_EditModalPartial", transactionVm);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var transactionDetailsVm = await _transactionService.GetTransactionDetailsAsync(id);

        return PartialView("_DeleteModalPartial", transactionDetailsVm);
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _transactionService.DeleteTransactionAsync(id);

        return RedirectToAction(nameof(Index));
    }

    public SelectList GetCategorySelectList(List<Category> categories)
    {
        return new SelectList(categories.Select(c => c.Type));
    }
}
