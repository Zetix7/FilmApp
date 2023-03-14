using FilmApp.Data.Entities;

namespace FilmApp.Data.Repositories;

public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T> where T : class, IEntity
{
}
