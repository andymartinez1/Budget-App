using BudgetApp.Models.ViewModels;
using BudgetApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BudgetApp.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    // GET: Category
    public async Task<IActionResult> Index(int? pageNumber = 1)
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        var categoryVm = new TransactionCategoryViewModel
        {
            Categories = categories,
        };
        categoryVm.SetCategories(categories);

        var pageSize = 7;
        var currentPage = pageNumber ?? 1;
        var totalCount = categories.Count();
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        // Set paged items
        categoryVm.Categories = categories
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        // Expose paging info to the view
        ViewBag.CurrentPage = currentPage;
        ViewBag.TotalPages = totalPages;

        return View(categoryVm);
    }

    // GET: Category/Create
    public async Task<IActionResult> Create()
    {
        var categoryVm = new CategoryViewModel();

        return PartialView("_CreateCategoryModalPartial", categoryVm);
    }

    // POST: Category/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("CategoryId,Type")] CategoryViewModel categoryVm)
    {
        if (ModelState.IsValid)
        {
            await _categoryService.AddCategoryAsync(categoryVm);

            return RedirectToAction(nameof(Index));
        }

        return PartialView("_CreateCategoryModalPartial", categoryVm);
    }

    // GET: Category/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var categoryToUpdate = await _categoryService.GetCategoryByIdAsync(id);

        var categoryVm = new CategoryViewModel(categoryToUpdate);

        return PartialView("_EditCategoryModalPartial", categoryVm);
    }

    // POST: Category/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("CategoryId,Type")] CategoryViewModel categoryVm
    )
    {
        if (ModelState.IsValid)
        {
            var categoryToUpdate = await _categoryService.GetCategoryByIdAsync(id);
            categoryToUpdate.Type = categoryVm.Type;

            await _categoryService.UpdateCategoryAsync(id);

            return RedirectToAction(nameof(Index));
        }

        return PartialView("_EditCategoryModalPartial", categoryVm);
    }

    // GET: Category/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);

        var categoryVm = new CategoryViewModel { Type = category.Type };

        return PartialView("_DeleteCategoryModalPartial", categoryVm);
    }

    // POST: Category/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _categoryService.DeleteCategoryAsync(id);

        return RedirectToAction(nameof(Index));
    }
}