using BudgetApp.Data;
using BudgetApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly BudgetDbContext _dbContext;

    public CategoryRepository(BudgetDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddCategoryAsync(Category category)
    {
        await _dbContext.Categories.AddAsync(category);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _dbContext.Categories.ToListAsync();
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        var category = await _dbContext.Categories.FindAsync(id);

        return category;
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        _dbContext.Categories.Update(category);

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var category = await GetCategoryByIdAsync(id);

        _dbContext.Categories.Remove(category);

        await _dbContext.SaveChangesAsync();
    }
}
