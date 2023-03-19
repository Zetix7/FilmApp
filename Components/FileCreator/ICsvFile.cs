using FilmApp.Components.FileCreator.Models;

namespace FilmApp.Components.FileCreator;

public interface ICsvFile
{
    void CreateArtistsCsvFile();
    void CreateMoviesCsvFile();
    List<Artist> ReadArtistsCsvFile(string pathName);
    List<Movie> ReadMoviesCsvFile(string pathName);
}
