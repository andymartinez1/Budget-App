using BudgetApp.Data;
using BudgetApp.Models;
using BudgetApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Controllers;

public class TransactionController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly BudgetDbContext _context;
    private readonly ITransactionService _transactionService;

    public TransactionController(
        BudgetDbContext context,
        ITransactionService transactionService,
        ICategoryService categoryService
    )
    {
        _context = context;
        _transactionService = transactionService;
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string filterCategory)
    {
        if (_context.Transactions == null)
            return Problem("Entity set is null");

        var transactions = await _transactionService.GetAllTransactionsAsync();
        var categories = await _categoryService.GetAllCategoriesAsync();

        var transactionVm = new TransactionCategoryViewModel
        {
            Categories = categories
                .OrderBy(c => c.Type)
                .Select(c => new SelectListItem { Value = c.CategoryId.ToString(), Text = c.Type })
                .ToList(),
            FilterCategory = filterCategory,
            Transactions = transactions,
        };

        return View(transactionVm);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var transaction = await _transactionService.GetTransactionByIdAsync(id);

        var transactionVm = new TransactionViewModel
        {
            Date = transaction.Date,
            Amount = transaction.Amount,
            CategoryId = transaction.CategoryId,
            Name = transaction.Name,
        };

        if (transactionVm == null)
            return NotFound();

        return PartialView("_DetailsModalPartial", transactionVm);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewData["CategoryId"] = new SelectList(
            _context.Categories.OrderBy(c => c.Type),
            "CategoryId",
            "Type"
        );
        return PartialView("_CreateModalPartial");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Transaction transaction)
    {
        if (ModelState.IsValid)
            await _transactionService.AddTransactionAsync(transaction);

        return PartialView("_CreateModalPartial", transaction);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var transaction = await _transactionService.GetTransactionByIdAsync(id);

        var transactionVm = new TransactionViewModel
        {
            Date = transaction.Date,
            Amount = transaction.Amount,
            CategoryId = transaction.CategoryId,
            Name = transaction.Name,
        };

        if (transactionVm == null)
            return NotFound();

        return PartialView("_EditModalPartial", transactionVm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("Id,Date,Name,Description,CategoryId,Amount")] Transaction transaction
    )
    {
        if (id != transaction.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(transaction);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(transaction.Id))
                    return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["CategoryId"] = new SelectList(
            _context.Categories,
            "Id",
            "Id",
            transaction.CategoryId
        );
        return PartialView("_EditModalPartial", transaction);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var transaction = await _context
            .Transactions.Include(t => t.Category)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (transaction == null)
            return NotFound();

        return PartialView("_DeleteModalPartial", transaction);
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction != null)
            _context.Transactions.Remove(transaction);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TransactionExists(int id)
    {
        return _context.Transactions.Any(e => e.Id == id);
    }

    private SelectList GetCategorySelectList(object selectedValue = null)
    {
        return new SelectList(
            _context.Categories.AsNoTracking().OrderBy(c => c.Type),
            "CategoryId",
            "Type",
            selectedValue
        );
    }
}
