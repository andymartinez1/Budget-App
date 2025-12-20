using BudgetApp.Models.ViewModels;
using BudgetApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> Index(string filterCategory, string searchString)
    {
        var transactions = await _transactionService.GetAllTransactionsAsync();
        var categories = await _categoryService.GetAllCategoriesAsync();

        var transactionVm = new TransactionCategoryViewModel
        {
            Categories = _categoryService.GetCategorySelectList(categories),
            FilterCategory = filterCategory,
            Transactions = transactions,
            SearchName = searchString,
        };

        return View(transactionVm);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var transaction = await _transactionService.GetTransactionByIdAsync(id);
        var category = await _categoryService.GetCategoryByIdAsync(transaction.CategoryId);

        var transactionVm = new TransactionViewModel
        {
            Amount = transaction.Amount,
            Category = category.Type,
            Date = transaction.Date,
            Name = transaction.Name,
        };

        return PartialView("_DetailsModalPartial", transactionVm);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();

        var transactionVm = new TransactionViewModel
        {
            Categories = _categoryService.GetCategorySelectList(categories),
        };

        return PartialView("_CreateModalPartial", transactionVm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TransactionViewModel transactionVm)
    {
        if (ModelState.IsValid)
        {
            var transaction = await _transactionService.AddTransactionAsync(transactionVm);

            var newTransactionVm = new TransactionViewModel
            {
                Id = transaction.TransactionId,
                Name = transaction.Name,
                Date = transaction.Date,
                Amount = transaction.Amount,
                CategoryId = transaction.CategoryId,
            };
        }

        return PartialView("_CreateModalPartial", transactionVm);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var transactionToUpdate = await _transactionService.GetTransactionByIdAsync(id);
        var categories = await _categoryService.GetAllCategoriesAsync();

        var transactionVm = new TransactionViewModel
        {
            Id = transactionToUpdate.TransactionId,
            Amount = transactionToUpdate.Amount,
            Date = transactionToUpdate.Date,
            Name = transactionToUpdate.Name,
            CategoryId = transactionToUpdate.CategoryId,
            Categories = _categoryService.GetCategorySelectList(categories),
        };

        return PartialView("_EditModalPartial", transactionVm);
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

            return RedirectToAction(nameof(Index));
        }

        return PartialView("_EditModalPartial", transactionVm);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var transaction = await _transactionService.GetTransactionByIdAsync(id);
        var category = await _categoryService.GetCategoryByIdAsync(transaction.CategoryId);

        var transactionVm = new TransactionViewModel
        {
            Amount = transaction.Amount,
            Category = category.Type,
            Date = transaction.Date,
            Name = transaction.Name,
        };

        return PartialView("_DeleteModalPartial", transactionVm);
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _transactionService.DeleteTransactionAsync(id);

        return RedirectToAction(nameof(Index));
    }
}
