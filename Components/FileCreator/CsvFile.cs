﻿using FilmApp.Components.DataProvider;
using FilmApp.Components.FileCreator.Models;

namespace FilmApp.Components.FileCreator;

public class CsvFile : ICsvFile
{
    private readonly IDataProvider _dataProvider;

    public CsvFile(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public void CreateArtistCsvFile()
    {
        using (var writer = File.CreateText(@"Resources\Files\artists.csv"))
        {
            foreach (var line in _dataProvider.GenerateSampleArtists())
            {
                writer.WriteLine($"{line.FirstName},{line.LastName}");
            }
        }
    }

    public void CreateMovieCsvFile()
    {
        using (var writer = File.CreateText(@"Resources\Files\movies.csv"))
        {
            foreach (var line in _dataProvider.GenerateSampleMovies())
            {
                writer.WriteLine($"{line.Title},{line.Year},{line.Universe},{line.BoxOffice}");
            }
        }
    }

    public List<Artist> ReadArtistCsvFile(string pathName)
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


    public List<Movie> ReadMovieCsvFile(string pathName)
    {
        var movies = File.ReadAllLines(pathName).Where(x => x.Length > 1).Select(x =>
        {
            var columns = x.Split(',');
            return new Movie
            {
                Title = columns[0],
                Year = int.Parse(columns[1]),
                Universe = columns[2],
                BoxOffice = decimal.Parse(columns[3])
            };
        });

        return movies.ToList();
    }
}
