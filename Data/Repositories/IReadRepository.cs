using FilmApp.Data.Entities;

namespace FilmApp.Data.Repositories;

public interface IReadRepository<in T> where T : class, IEntity
{
    void Add(T entity);
    void Remove(T entity);
    void Save();
}