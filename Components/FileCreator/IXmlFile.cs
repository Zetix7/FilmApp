using FilmApp.Components.FileCreator.Models;

namespace FilmApp.Components.FileCreator;

public interface IXmlFile
{
    void CreateArtistsXmlFile();
    void CreateMoviesXmlFile();
    List<Artist> ReadArtistsXmlFile(string pathName);
    List<Movie> ReadMoviesXmlFile(string pathName);
}