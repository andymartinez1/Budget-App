using BudgetApp.Models;
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
    public async Task<IActionResult> Index(string searchString)
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        var categoryVm = new TransactionCategoryViewModel
        {
            Categories = categories,
            CategoriesSelectList = _categoryService.GetCategorySelectList(categories),
            SearchName = searchString,
        };

        return View(categoryVm);
    }

    // GET: Category/Create
    public async Task<IActionResult> Create()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();

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
            var category = new Category { Type = categoryVm.Type };

            await _categoryService.AddCategoryAsync(category);

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
