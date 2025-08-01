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
        public async Task<ActionResult> Index(string transactionCategory, string searchString)
        {
            if (_context.Transactions == null)
                return Problem("Entity set is null");

            var transactionQuery = _context.Transactions.Include(t => t.Category).AsQueryable();

            var transactions = await transactionQuery.ToListAsync();
            var categories = await _context.Categories.ToListAsync();
            var transactionVM = new TransactionViewModel
            {
                Transactions = transactions,
                Categories = categories,
                TransactionCategory = new SelectList(
                    categories
                        .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Type })
                        .ToList(),
                    "Value",
                    "Text"
                ),
                SearchString = searchString,
            };
            return View(transactionVM);
        }

        // GET: TransactionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TransactionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TransactionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
