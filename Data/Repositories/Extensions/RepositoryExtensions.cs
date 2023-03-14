using FilmApp.Data.Entities;

namespace FilmApp.Data.Repositories.Extensions;

public static class RepositoryExtensions
{
    public static IRepository<T> AddBatch<T>(this IRepository<T> repository, List<T> list) where T : class, IEntity, new()
    {
        foreach (var item in list)
        {
            repository.Add(item);
        }

        return repository;
    }
}
