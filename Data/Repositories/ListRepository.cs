using FilmApp.Data.Entities;

namespace FilmApp.Data.Repositories;

public class ListRepository<T> : IRepository<T> where T : class, IEntity, new()
{
    private readonly List<T> _repository = new();

    public IEnumerable<T> GetAll()
    {
        return _repository.ToList();
    }

    public T? GetById(int id)
    {
        return _repository.FirstOrDefault(x=>x.Id == id);
    }

    public void Add(T entity)
    {
        entity.Id = _repository.Count + 1;
        _repository.Add(entity);
    }

    public void Remove(T entity)
    {
        _repository.Remove(entity);
    }

    public void Save()
    {
        foreach (T entity in GetAll())
        {
            Console.WriteLine(entity);
        }
    }
}
