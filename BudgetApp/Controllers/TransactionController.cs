using BudgetApp.Data;
using BudgetApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Controllers
{
    public class TransactionController : Controller
    {
        private readonly BudgetDbContext _context;

        public TransactionController(BudgetDbContext context)
        {
            _context = context;
        }

        // GET: TransactionController
        public async Task<ActionResult> Index(string searchString)
        {
            if (_context.Transactions == null)
                return Problem("Entity set is null");

            var transactions = await _context.Transactions.ToListAsync();
            var categories = await _context.Categories.ToListAsync();
            var category = categories.Select(c => c.Type);

            var transactionVM = new TransactionViewModel
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
            return View(transactionVM);
        }

        public async Task<ActionResult> TransactionDetails(int id)
        {
            if (id == null)
                return NotFound();

            var transactionDetails = await _context.Transactions.FirstOrDefaultAsync(t =>
                t.Id == id
            );
            if (transactionDetails == null)
                return NotFound();

            return PartialView("_Details", transactionDetails);
        }

        // GET: TransactionController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        // POST: TransactionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Transaction transaction)
        {
            var newTransaction = new Transaction
            {
                Name = transaction.Name,
                Description = transaction.Description,
                Amount = transaction.Amount,
                Category = transaction.Category,
                Date = transaction.Date,
            };

            return PartialView("_Create", newTransaction);
        }

        // GET: TransactionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TransactionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TransactionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TransactionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
