using BudgetApp.Models;
using BudgetApp.Models.ViewModels;
using BudgetApp.Repository.Interfaces;
using BudgetApp.Services.Interfaces;

namespace BudgetApp.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task AddCategoryAsync(CategoryViewModel categoryVm)
    {
        var category = new Category { Type = categoryVm.Type };

        await _categoryRepository.AddCategoryAsync(category);
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _categoryRepository.GetAllCategoriesAsync();
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(id);

        return category;
    }

    public async Task DeleteCategoryAsync(int id)
    {
        await _categoryRepository.DeleteCategoryAsync(id);
    }

    public async Task UpdateCategoryAsync(int id)
    {
        var category = await GetCategoryByIdAsync(id);

        await _categoryRepository.UpdateCategoryAsync(category);
    }
}
