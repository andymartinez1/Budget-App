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
    public async Task<IActionResult> Index(
        string filterCategory,
        string searchString,
        string startDate,
        string endDate,
        int? pageNumber = 1
    )
    {
        var transactions = await _transactionService.GetAllTransactionsAsync();
        var categories = await _categoryService.GetAllCategoriesAsync();

        // Apply filters
        if (!string.IsNullOrEmpty(filterCategory))
            transactions = transactions
                .Where(t =>
                    t.CategoryId.ToString() == filterCategory
                    || (t.Category != null && t.Category.Type == filterCategory)
                )
                .ToList();

        if (!string.IsNullOrEmpty(searchString))
            transactions = transactions
                .Where(t =>
                    t.Name != null
                    && t.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                )
                .ToList();

        if (!string.IsNullOrEmpty(startDate) && DateTime.TryParse(startDate, out var start))
            transactions = transactions.Where(t => t.Date.Date >= start.Date).ToList();

        if (!string.IsNullOrEmpty(endDate) && DateTime.TryParse(endDate, out var end))
        {
            var endOfDay = end.Date.AddDays(1).AddTicks(-1);
            transactions = transactions.Where(t => t.Date <= endOfDay).ToList();
        }

        var transactionVm = new TransactionCategoryViewModel
        {
            FilterCategory = filterCategory,
            Transactions = transactions,
            SearchName = searchString,
            SearchStartDate = startDate,
            SearchEndDate = endDate
        };
        transactionVm.SetCategories(categories);

        var pageSize = 7;
        var currentPage = pageNumber ?? 1;
        var totalCount = transactions.Count();
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        // Set paged items
        transactionVm.Transactions = transactions
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        // Expose paging info to the view
        ViewBag.CurrentPage = currentPage;
        ViewBag.TotalPages = totalPages;

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
            CategoryType = category.Type,
            Date = transaction.Date,
            Name = transaction.Name
        };

        return PartialView("_DetailsModalPartial", transactionVm);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();

        var transactionVm = new TransactionViewModel(categories);

        return PartialView("_CreateModalPartial", transactionVm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("TransactionId, Name, Date, Amount, CategoryId")]
        TransactionViewModel transactionVm
    )
    {
        var created = await _transactionService.AddTransactionAsync(transactionVm);

        if (created == null)
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            transactionVm.SetCategories(categories);
            ModelState.AddModelError(string.Empty, "Unable to create transaction. Try again.");
            return PartialView("_CreateModalPartial", transactionVm);
        }

        // return RedirectToAction(nameof(Index));
        return Created();
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var transactionToUpdate = await _transactionService.GetTransactionByIdAsync(id);
        var categories = await _categoryService.GetAllCategoriesAsync();

        var transactionVm = new TransactionViewModel(transactionToUpdate);
        transactionVm.SetCategories(categories);

        return PartialView("_EditModalPartial", transactionVm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("TransactionId,Name,Date,Amount,CategoryId,Category")]
        TransactionViewModel transactionVm
    )
    {
        var transactionToUpdate = await _transactionService.GetTransactionByIdAsync(id);
        transactionToUpdate.TransactionId = id;
        transactionToUpdate.Name = transactionVm.Name;
        transactionToUpdate.Amount = transactionVm.Amount;
        transactionToUpdate.Category = transactionVm.Category;
        transactionToUpdate.Date = transactionVm.Date;
        transactionToUpdate.CategoryId = transactionVm.CategoryId;

        await _transactionService.UpdateTransactionAsync(id);

        return Ok(transactionToUpdate);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var transaction = await _transactionService.GetTransactionByIdAsync(id);
        var category = await _categoryService.GetCategoryByIdAsync(transaction.CategoryId);

        var transactionVm = new TransactionViewModel
        {
            Amount = transaction.Amount,
            CategoryType = category.Type,
            Date = transaction.Date,
            Name = transaction.Name
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