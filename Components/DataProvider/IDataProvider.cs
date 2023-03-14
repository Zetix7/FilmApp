using FilmApp.Data.Entities;

namespace FilmApp.Components.DataProvider;

public interface IDataProvider
{
    List<Artist> GenerateSampleArtists();
    List<Movie> GenerateSampleMovies();
}
