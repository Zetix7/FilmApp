using FilmApp.Components.FileCreator.Models;

namespace FilmApp.Components.FileCreator;

public interface IXmlFile
{
    void CreateArtistsXmlFileFromCsvFile();
    void CreateMoviesXmlFileFromCsvFile();
    List<ArtistInFile> ReadArtistsXmlFile(string pathName);
    List<MovieInFile> ReadMoviesXmlFile(string pathName);
}