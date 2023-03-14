using FilmApp.Components.FileCreator.Models;

namespace FilmApp.Components.FileCreator;

public interface IXmlFile
{
    void CreateArtistXmlFile();
    void CreateMovieXmlFile();
    List<Artist> ReadArtistXmlFile(string pathName);
    List<Movie> ReadMovieXmlFile(string pathName);
}