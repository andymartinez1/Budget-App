using BudgetApp.Data;
using BudgetApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Controllers;

public class CategoryController : Controller
{
    private readonly BudgetDbContext _context;

    public CategoryController(BudgetDbContext context)
    {
        _context = context;
    }

    // GET: Category
    public async Task<IActionResult> Index(string searchString)
    {
        if (_context.Categories == null)
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

    // GET: Category/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
        if (category == null)
            return NotFound();

        return View(category);
    }

    // GET: Category/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Category/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Type")] Category category)
    {
        if (ModelState.IsValid)
        {
            _context.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(category);
    }

    // GET: Category/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var category = await _context.Categories.FindAsync(id);
        if (category == null)
            return NotFound();

        return View(category);
    }

    // POST: Category/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Type")] Category category)
    {
        if (id != category.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(category);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(category.Id))
                    return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(category);
    }

    // GET: Category/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
        if (category == null)
            return NotFound();

        return View(category);
    }

    // POST: Category/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category != null)
            _context.Categories.Remove(category);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CategoryExists(int id)
    {
        return _context.Categories.Any(e => e.Id == id);
    }
}
