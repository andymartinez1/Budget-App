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
        throw new NotImplementedException();
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _dbContext.Categories.ToListAsync();
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteCategoryAsync(int id)
    {
        throw new NotImplementedException();
    }
}
