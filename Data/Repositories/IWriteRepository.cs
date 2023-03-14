using FilmApp.Data.Entities;

namespace FilmApp.Data.Repositories;

public interface IWriteRepository<out T> where T : class, IEntity
{
    IEnumerable<T> GetAll();
    T? GetById(int id);
}