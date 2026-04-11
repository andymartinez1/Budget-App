namespace BudgetApp.ServiceContracts;

public interface ICrudService<TModel, TRequest, TKey>
    where TKey : notnull
{
    public Task<TModel> AddAsync(TRequest request);

    public Task<List<TModel>> GetAllAsync();

    public Task<TModel> GetByIdAsync(TKey id);

    public Task<TModel> UpdateAsync(TKey id);

    public Task DeleteAsync(TKey id);
}
