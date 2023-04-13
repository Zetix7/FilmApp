using FilmApp.Components.FileCreator.Models;

namespace FilmApp.Components.FileCreator;

public interface ICsvFile
{
    void CreateArtistsCsvFile();
    void CreateMoviesCsvFile();
    List<ArtistInFile> ReadArtistsCsvFile(string pathName);
    List<MovieInFile> ReadMoviesCsvFile(string pathName);
}
