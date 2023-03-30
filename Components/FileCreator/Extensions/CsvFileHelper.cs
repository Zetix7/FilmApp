using FilmApp.Data.Entities;

namespace FilmApp.Components.FileCreator.Extensions;

public class CsvFileHelper
{
    public static void SaveMoviesToCsvFile(StreamWriter writer, List<Movie> movies)
    {
        foreach (var movie in movies)
        {
            var convertedBoxOffice = ConvertFormatBoxOffice(movie.BoxOffice);
            writer.WriteLine($"{movie.Title},{movie.Year},{movie.Universe},{convertedBoxOffice}");
        }
    }

    private static string ConvertFormatBoxOffice(decimal boxOffice)
    {
        var toConvertBoxOffice = boxOffice.ToString().Split(',');
        return toConvertBoxOffice.Length > 1 ? toConvertBoxOffice[0] + "." + toConvertBoxOffice[1] : toConvertBoxOffice[0] + ".00";
    }
}
