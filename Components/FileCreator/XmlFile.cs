using FilmApp.Components.FileCreator.Models;
using System.Globalization;
using System.Xml.Linq;

namespace FilmApp.Components.FileCreator;

public class XmlFile : IXmlFile
{
    private readonly ICsvFile _csvFile;

    public XmlFile(ICsvFile csvFile)
    {
        _csvFile = csvFile;
    }

    public void CreateArtistsXmlFileFromCsvFile()
    {
        var csvArtists = _csvFile.ReadArtistsCsvFile(@"Resources\Files\artists.csv");

        var xmlArtists = new XElement("Artists", csvArtists
            .Select(x => new XElement("Artist",
                    new XAttribute("FirstName", x.FirstName!),
                    new XAttribute("LastName", x.LastName!))));

        var xmlFile = new XDocument(xmlArtists);
        xmlArtists.Save(@"Resources\Files\artists.xml");
    }

    public void CreateMoviesXmlFileFromCsvFile()
    {
        var csvMovies = _csvFile.ReadMoviesCsvFile(@"Resources\Files\movies.csv");

        var xmlMovies = new XElement("Movies", csvMovies
            .Select(x => new XElement("Movie",
                new XAttribute("Title", x.Title!),
                new XAttribute("Year", x.Year),
                new XAttribute("Universe", x.Universe!),
                new XAttribute("BoxOffice", x.BoxOffice))));

        var xmlFile = new XDocument(xmlMovies);
        xmlFile.Save(@"Resources\Files\movies.xml");
    }

    public List<Artist> ReadArtistsXmlFile(string pathName)
    {
        var records = XDocument.Load(pathName);

        var artists = records.Element("Artists")!.Elements("Artist").Select(x => new Artist
        {
            FirstName = x.Attribute("FirstName")!.Value,
            LastName = x.Attribute("LastName")!.Value
        }).ToList();

        return artists;
    }

    public List<Movie> ReadMoviesXmlFile(string pathName)
    {
        var records = XDocument.Load(pathName);

        return records.Element("Movies")!.Elements("Movie").Select(x => new Movie
        {
            Title = x.Attribute("Title")!.Value,
            Year = int.Parse(x.Attribute("Year")!.Value, CultureInfo.InvariantCulture),
            Universe = x.Attribute("Universe")!.Value,
            BoxOffice = decimal.Parse(x.Attribute("BoxOffice")!.Value, CultureInfo.InvariantCulture)
        }).ToList();
    }
}
