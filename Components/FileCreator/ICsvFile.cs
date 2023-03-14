using FilmApp.Components.FileCreator.Models;

namespace FilmApp.Components.FileCreator;

public interface ICsvFile
{
    void CreateArtistCsvFile();
    void CreateMovieCsvFile();
    List<Artist> ReadArtistCsvFile(string pathName);
    List<Movie> ReadMovieCsvFile(string pathName);
}
