using BudgetApp.Data;
using BudgetApp.Entities;
using BudgetApp.Models;
using BudgetApp.ServiceContracts;
using BudgetApp.ServiceContracts.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BudgetApp.Services;

public class CategoryService : ICategoryService
{
    private readonly BudgetDbContext _context;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(ILogger<CategoryService> logger, BudgetDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<Category> AddAsync(CategoryViewModel categoryVm)
    {
        var category = new Category { Name = categoryVm.Name };

        await _context.Categories.AddAsync(category);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e)
        {
            _logger.LogWarning(e, "Concurrency conflict while adding category.");
            return category;
        }
        catch (DbUpdateException e)
        {
            _logger.LogError(e, "Database update failed while adding category.");
            return category;
        }

        _logger.LogInformation("Category '{Name}' created.", category.Name);
        return category;
    }

    public async Task<List<Category>> GetAllAsync()
    {
        return await _context.Categories.AsNoTracking().ToListAsync();
    }

    public async Task<Category> GetByIdAsync(int id)
    {
        var category = await _context
            .Categories.AsNoTracking()
            .SingleOrDefaultAsync(c => c.Id == id);

        if (category is null)
            return null;

        return category;
    }

    public async Task<Category> UpdateAsync(int id, CategoryViewModel vm)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category is null)
        {
            _logger.LogWarning(
                "Update requested for category {CategoryId}, but it was not found.",
                id
            );
            return null;
        }

        category.Name = vm.Name;

        _context.Categories.Update(category);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e)
        {
            _logger.LogWarning(e, "Concurrency conflict while updating category.");
            return category;
        }
        catch (DbUpdateException e)
        {
            _logger.LogError(e, "Database update failed while updating category.");
            return category;
        }

        _logger.LogInformation("Category '{Name}' updated.", category.Name);
        return category;
    }

    public async Task<DeleteResult> DeleteAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category is null)
        {
            _logger.LogWarning(
                "Delete requested for category {CategoryId}, but it was not found.",
                id
            );
            return DeleteResult.NotFoundResult("Category not found.");
        }

        _context.Categories.Remove(category);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e)
        {
            _logger.LogWarning(e, "Concurrency conflict while deleting category {CategoryId}.", id);
            return DeleteResult.ConflictResult(
                "The category was modified or deleted by another process."
            );
        }
        catch (DbUpdateException e)
        {
            _logger.LogError(e, "Database update failed while deleting category {CategoryId}.", id);
            return DeleteResult.Failure("Unable to delete the category.");
        }

        _logger.LogInformation("Category '{Name}' deleted.", category.Name);
        return DeleteResult.Success();
    }
}
