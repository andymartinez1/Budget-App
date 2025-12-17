using BudgetApp.Data;
using BudgetApp.Models;
using BudgetApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Controllers;

public class TransactionController : Controller
{
    private readonly BudgetDbContext _context;
    private readonly ITransactionService _service;

    public TransactionController(BudgetDbContext context, ITransactionService service)
    {
        _context = context;
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string searchString)
    {
        if (_context.Transactions == null)
            return Problem("Entity set is null");

        var transactions = await _context.Transactions.ToListAsync();
        var categories = await _context.Categories.ToListAsync();
        var category = categories.Select(c => c.Type);

        var transactionVm = new TransactionViewModel
        {
            Transactions = transactions,
            Categories = categories,
            TransactionCategories = new SelectList(
                categories
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Type })
                    .ToList(),
                "Value",
                "Text"
            ),
            TransactionCategory = category.ToString(),
            SearchString = searchString,
        };
        return View(transactionVm);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var transaction = await _context
            .Transactions.Include(t => t.Category)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (transaction == null)
            return NotFound();

        return PartialView("_DetailsModalPartial", transaction);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
        return PartialView("_CreateModalPartial");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Id,Date,Name,Description,CategoryId,Amount")] Transaction transaction
    )
    {
        if (ModelState.IsValid)
        {
            _context.Add(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["CategoryId"] = new SelectList(
            _context.Categories,
            "Id",
            "Id",
            transaction.CategoryId
        );
        return PartialView("_CreateModalPartial", transaction);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction == null)
            return NotFound();

        ViewData["CategoryId"] = new SelectList(
            _context.Categories,
            "Id",
            "Id",
            transaction.CategoryId
        );
        return PartialView("_EditModalPartial", transaction);
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
}
