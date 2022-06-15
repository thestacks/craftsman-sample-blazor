namespace RecipeManagement.Services;

using RecipeManagement.Databases;

public interface IUnitOfWork : IRecipeManagementService
{
    Task CommitChanges(CancellationToken cancellationToken = default);
}

public class UnitOfWork : IUnitOfWork
{
    private readonly RecipesDbContext _dbContext;

    public UnitOfWork(RecipesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CommitChanges(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
