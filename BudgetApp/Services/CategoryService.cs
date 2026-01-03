using BudgetApp.Models;
using BudgetApp.Models.ViewModels;
using BudgetApp.Repository.Interfaces;
using BudgetApp.Services.Interfaces;

namespace BudgetApp.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(ICategoryRepository categoryRepository, ILogger<CategoryService> logger)
    {
        _categoryRepository = categoryRepository;
        _logger = logger;
    }

    public async Task AddCategoryAsync(CategoryViewModel categoryVm)
    {
        var category = new Category { Type = categoryVm.Type };

        await _categoryRepository.AddCategoryAsync(category);
        _logger.LogInformation("Category '{Type}' created.", category.Type);
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _categoryRepository.GetAllCategoriesAsync();
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(id);
        _logger.LogInformation("Category with ID: {CategoryId} retrieved.", category.CategoryId);

        return category;
    }

    public async Task DeleteCategoryAsync(int id)
    {
        await _categoryRepository.DeleteCategoryAsync(id);
        _logger.LogInformation("Category with ID: {CategoryId} deleted.", id);
    }

    public async Task UpdateCategoryAsync(int id)
    {
        var category = await GetCategoryByIdAsync(id);

        await _categoryRepository.UpdateCategoryAsync(category);
        _logger.LogInformation("Category with ID: {CategoryId} updated.", category.CategoryId);
    }
}
