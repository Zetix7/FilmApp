using FilmApp.Components.DataProvider;
using FilmApp.Components.FileCreator.Models;
using System.Globalization;

namespace FilmApp.Components.FileCreator;

public class CsvFile : ICsvFile
{
    private readonly IDataProvider _dataProvider;

    public CsvFile(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public void CreateArtistsCsvFile()
    {
        using var writer = File.CreateText(@"Resources\Files\artists.csv");
        foreach (var line in _dataProvider.GenerateSampleArtists())
        {
            writer.WriteLine($"{line.FirstName},{line.LastName}");
        }
    }

    public void CreateMoviesCsvFile()
    {
        using var writer = File.CreateText(@"Resources\Files\movies.csv");
        foreach (var line in _dataProvider.GenerateSampleMovies())
        {
            writer.WriteLine($"{line.Title},{line.Year},{line.Universe},{line.BoxOffice}");
        }
    }

    public List<Artist> ReadArtistsCsvFile(string pathName)
    {
        var artists = File.ReadAllLines(pathName).Where(x => x.Length > 1).Select(x =>
         {
             var columns = x.Split(',');
             return new Artist
             {
                 FirstName = columns[0],
                 LastName = columns[1]
             };
         });

        return artists.ToList();
    }


    public List<Movie> ReadMoviesCsvFile(string pathName)
    {
        var movies = File.ReadAllLines(pathName).Where(x => x.Length > 1).Select(x =>
        {
            var columns = x.Split(',');
            return new Movie
            {
                Title = columns[0],
                Year = int.Parse(columns[1], CultureInfo.InvariantCulture),
                Universe = columns[2],
                BoxOffice = decimal.Parse(columns[3], CultureInfo.InvariantCulture)
            };
        });

        return movies.ToList();
    }
}
