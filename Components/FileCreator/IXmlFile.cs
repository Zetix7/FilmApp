using FilmApp.Components.FileCreator.Models;

namespace FilmApp.Components.FileCreator;

public interface IXmlFile
{
    void CreateArtistsXmlFileFromCsvFile();
    void CreateMoviesXmlFileFromCsvFile();
    List<Artist> ReadArtistsXmlFile(string pathName);
    List<Movie> ReadMoviesXmlFile(string pathName);
}