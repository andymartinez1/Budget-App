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

    public async Task AddCategory(Category category)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Category>> GetAllCategories()
    {
        return await _dbContext.Categories.ToListAsync();
    }

    public async Task<Category> GetCategoryById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateCategory(Category category)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteCategory(int id)
    {
        throw new NotImplementedException();
    }
}
