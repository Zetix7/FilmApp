using FilmApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmApp.Data.Repositories;

public class SqlRepository<T> : IRepository<T> where T : class, IEntity
{
    private readonly FilmAppDbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public SqlRepository(FilmAppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public IEnumerable<T> GetAll() => _dbSet.ToList();

    public T? GetById(int id) => _dbSet.SingleOrDefault(x => x.Id == id);

    public void Add(T entity) => _dbSet.Add(entity);

    public void Remove(T entity) => _dbSet.Remove(entity);

    public void Save() => _dbContext.SaveChanges();
}
