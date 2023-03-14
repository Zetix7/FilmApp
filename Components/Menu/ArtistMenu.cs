using FilmApp.Components.FileCreator;
using FilmApp.Data.Entities;
using FilmApp.Data.Repositories;
using System.Xml;
using System.Xml.Linq;

namespace FilmApp.Components.Menu;

public class ArtistMenu : Menu<Artist>, IMenu<Artist>
{
    private readonly IRepository<Artist> _repository;
    private readonly ICsvFile _csvFile;
    private readonly IXmlFile _xmlFile;

    public ArtistMenu(IRepository<Artist> repository, ICsvFile csvFile, IXmlFile xmlFile) : base(repository)
    {
        _repository = repository;
        _csvFile = csvFile;
        _xmlFile = xmlFile;
    }

    public void LoadMenu()
    {
        while (true)
        {
            Console.WriteLine("Artists - What do you want to do?");
            Console.WriteLine("\tChoose one option:");
            Console.WriteLine("\t\t1 - Read all artists");
            Console.WriteLine("\t\t2 - Read artist by ID");
            Console.WriteLine("\t\t3 - Add new artist");
            Console.WriteLine("\t\t4 - Remove artist");
            Console.WriteLine("\t\t5 - Save changes");
            Console.WriteLine("\t\t6 - Read from artists.csv file");
            Console.WriteLine("\t\t7 - Save to artists.csv file");
            Console.WriteLine("\t\t8 - Read from artists.xml file");
            Console.WriteLine("\t\t9 - Save to artists.xml file");
            Console.WriteLine("\t\tQ - Exit");
            Console.Write("\tYour choise: ");
            var choise = Console.ReadLine()!.ToUpper();

            if (choise == "1")
            {
                ReadAllItems();
            }
            else if (choise == "2")
            {
                ReadItemById();
            }
            else if (choise == "3")
            {
                AddNewItem();
            }
            else if (choise == "4")
            {
                RemoveItem();
            }
            else if (choise == "5")
            {
                SaveChangesInDatabase();
            }
            else if (choise == "6")
            {
                ReadArtistFromCsvFile(@"Resources\Files\artists.csv");
            }
            else if (choise == "7")
            {
                SaveArtistsToCsvFile();
            }
            else if (choise == "8")
            {
                ReadArtistFromXmlFile(@"Resources\Files\artists.xml");
            }
            else if (choise == "9")
            {
                SaveArtistsToXmlFile();
            }
            else if (choise == "Q")
            {
                AddSeparator();
                break;
            }
            else
            {
                AddSeparator();
                Console.WriteLine("ERROR : Wrong option! \n\t\tChoose one option: 1 or 2 or 3 or 4 or 5 or 6 or 7 or 8 or 9 or Q! \n" +
                    "\tIf not, You will stuck here forever!");
                AddSeparator();
            }
        }
    }

    private void SaveArtistsToXmlFile()
    {
        var artists = _repository.GetAll().ToList();

        AddSeparator();
        if (artists.Count() > 0)
        {
            var data = new XElement("Artists", artists.Select(x =>
                new XElement("Artist",
                    new XAttribute("FirstName", x.FirstName),
                    new XAttribute("LastName", x.LastName))));

            var xmlFile = new XDocument(data);
            xmlFile.Save(@"Resources\Files\artists.xml");

            Console.WriteLine("INFO : Data from databese succesfully saved to artist.xml file!");
        }
        else
        {
            Console.WriteLine("INFO : Database is empty!");
        }
        AddSeparator();
    }

    private void ReadArtistFromXmlFile(string pathName)
    {
        try
        {
            AddSeparator();
            if (!File.Exists(pathName))
            {
                throw new FileNotFoundException($"ERROR : Directory or file '{pathName}' not exists!");
            }

            var artists = _xmlFile.ReadArtistXmlFile(pathName);
            ReadArtists(artists);
        }
        catch (FileNotFoundException fe)
        {
            Console.WriteLine(fe.Message);
        }
        catch (XmlException)
        {
            Console.WriteLine($"ERROR : Wrong or broken '{pathName}' file");
        }
        catch (NullReferenceException)
        {
            Console.WriteLine($"ERROR : Wrong or broken '{pathName}' file");
        }
        finally
        {
            AddSeparator();
        }
    }

    private void ReadArtists(List<FileCreator.Models.Artist> artists)
    {
        var count = 0;
        foreach (var artist in artists)
        {
            if (_repository.GetAll().Where(x => x.FirstName == artist.FirstName && x.LastName == artist.LastName).Any())
            {
                continue;
            }
            _repository.Add(new Artist
            {
                FirstName = artist.FirstName,
                LastName = artist.LastName,
            });
            count++;
            Console.WriteLine($"\t{artist.FirstName}, {artist.LastName}");
        }

        if (count > 0)
        {
            AddSeparator();
            Console.WriteLine("INFO : Data succesfully read and prepered to save in database! \n" +
                "\tDo not forget save changes, If you want save it to database!");
        }
        else
        {
            Console.WriteLine("INFO : No records to save in database!");
        }
    }

    private void SaveArtistsToCsvFile()
    {
        var artists = _repository.GetAll().ToList();

        AddSeparator();
        if (artists.Count > 0)
        {
            using (var csvFile = File.CreateText(@"Resources\Files\artists.csv"))
            {
                foreach (var artist in artists)
                {
                    csvFile.WriteLine($"{artist.FirstName},{artist.LastName}");
                }
            }

            Console.WriteLine("INFO : Data from databese succesfully saved to artists.csv file!");
        }
        else
        {
            Console.WriteLine("INFO : Database is empty!");
        }
        AddSeparator();
    }

    private void ReadArtistFromCsvFile(string pathName)
    {
        try
        {
            AddSeparator();
            if (!File.Exists(pathName))
            {
                throw new FileNotFoundException($"ERROR : Directory or file '{pathName}' not exists!");
            }

            var artists = _csvFile.ReadArtistCsvFile(pathName);
            ReadArtists(artists);
        }
        catch (FileNotFoundException fe)
        {
            Console.WriteLine(fe.Message);
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine($"ERROR : Wrong or broken '{pathName}' file");
        }
        finally
        {
            AddSeparator();
        }
    }

    protected override void RemoveItem()
    {
        try
        {
            ReadAllItems();
            Console.Write("\tChoose one ID from list above: ");
            var choise = Console.ReadLine()!.Trim();
            if (!int.TryParse(choise, out int id))
            {
                throw new FormatException($"ERROR : Invalid format! Insert MUST be a digit!");
            }

            var artist = _repository.GetById(id) ?? throw new ArgumentException($"ERROR : Invalid value! ID not exists! Try again!");
            _repository.Remove(artist!);

            AddSeparator();
            Console.WriteLine($"INFO :\tArtist {artist.FirstName} {artist.LastName} removed succesfully!\n\tDo not forget save changes!");
        }
        catch (FormatException fe)
        {
            AddSeparator();
            Console.WriteLine(fe.Message);
        }
        catch (ArgumentException ae)
        {
            AddSeparator();
            Console.WriteLine(ae.Message);
        }
        finally
        {
            AddSeparator();
        }
    }

    protected override void AddNewItem()
    {
        try
        {
            AddSeparator();
            Console.WriteLine("Add new artist:");
            Console.Write("\tFirstName: ");
            var firstName = Console.ReadLine()!.Trim();
            firstName = PascalFormat(firstName);

            Console.Write("\tLastName: ");
            var lastName = Console.ReadLine()!.Trim();
            lastName = PascalFormat(lastName);

            if (_repository.GetAll().Where(x => x.FirstName == firstName && x.LastName == lastName).Any())
            {
                throw new ArgumentException("ERROR : Artist exists in database! You can not add same artist!");
            }

            _repository.Add(new Artist { FirstName = firstName, LastName = lastName });

            AddSeparator();
            Console.WriteLine($"INFO :\tArtist {firstName} {lastName} added succesfully! Do not forget save changes!");
        }
        catch (ArgumentException ae)
        {
            AddSeparator();
            Console.WriteLine(ae.Message);
        }
        finally
        {
            AddSeparator();
        }
    }
}
