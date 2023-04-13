using FilmApp.Components.FileCreator;
using FilmApp.Components.FileCreator.Models;
using FilmApp.Components.Menu.Extensions;
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

    public new void LoadMenu()
    {
        while (true)
        {
            base.LoadMenu();
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
                ReadArtistsFromCsvFile(@"Resources\Files\artists.csv");
            }
            else if (choise == "6")
            {
                SaveArtistsToCsvFile();
            }
            else if (choise == "7")
            {
                ReadArtistsFromXmlFile(@"Resources\Files\artists.xml");
            }
            else if (choise == "8")
            {
                SaveArtistsToXmlFile();
            }
            else if (choise == "Q")
            {
                MenuHelper.AddSeparator();
                break;
            }
            else
            {
                MenuHelper.AddSeparator();
                Console.WriteLine("ERROR : Wrong option! \n\t\tChoose one option: 1 or 2 or 3 or 4 or 5 or 6 or 7 or 8 or Q! \n" +
                    "\tIf not, You will stuck here forever!");
                MenuHelper.AddSeparator();
            }
        }
    }

    private void SaveArtistsToXmlFile()
    {
        var artists = _repository.GetAll().ToList();

        MenuHelper.AddSeparator();
        if (artists.Count > 0)
        {
            var data = new XElement("Artists", artists
                .Select(x => new XElement("Artist",
                    new XAttribute("FirstName", x.FirstName!),
                    new XAttribute("LastName", x.LastName!))));

            var xmlFile = new XDocument(data);
            xmlFile.Save(@"Resources\Files\artists.xml");

            Console.WriteLine("INFO : Data succesfully saved to artists.xml file!");
        }
        else
        {
            Console.WriteLine("INFO : No data to save!");
        }
        MenuHelper.AddSeparator();
    }

    private void ReadArtistsFromXmlFile(string pathName)
    {
        MenuHelper.AddSeparator();
        try
        {
            if (!File.Exists(pathName))
            {
                throw new FileNotFoundException($"ERROR : Directory or file '{pathName}' not exists!");
            }

            var artists = _xmlFile.ReadArtistsXmlFile(pathName);
            AddArtistsToRepository(artists);
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
            MenuHelper.AddSeparator();
        }
    }

    private void AddArtistsToRepository(List<ArtistInFile> artists)
    {
        var isAddedNewArtistToRepository = false;
        foreach (var artist in artists)
        {
            if (_repository.GetAll().Where(x => x.FirstName == artist.FirstName && x.LastName == artist.LastName).Any())
            {
                continue;
            }
            _repository.Add(new Artist
            {
                FirstName = artist.FirstName!,
                LastName = artist.LastName!,
            });

            _repository.Save();
            isAddedNewArtistToRepository = true;
            Console.WriteLine($"\t{artist.FirstName}, {artist.LastName}");
        }

        if (isAddedNewArtistToRepository)
        {
            MenuHelper.AddSeparator();
            Console.WriteLine("INFO : Data succesfully read and saved!");
        }
        else
        {
            Console.WriteLine("INFO : No records to save!");
        }
    }

    private void SaveArtistsToCsvFile()
    {
        var artists = _repository.GetAll().ToList();

        MenuHelper.AddSeparator();
        if (artists.Count > 0)
        {
            using (var csvFile = File.CreateText(@"Resources\Files\artists.csv"))
            {
                foreach (var artist in artists)
                {
                    csvFile.WriteLine($"{artist.FirstName},{artist.LastName}");
                }
            }

            Console.WriteLine("INFO : Data succesfully saved to artists.csv file!");
        }
        else
        {
            Console.WriteLine("INFO : No data to save!");
        }
        MenuHelper.AddSeparator();
    }

    private void ReadArtistsFromCsvFile(string pathName)
    {
        try
        {
            MenuHelper.AddSeparator();
            if (!File.Exists(pathName))
            {
                throw new FileNotFoundException($"ERROR : Directory or file '{pathName}' not exists!");
            }

            var artists = _csvFile.ReadArtistsCsvFile(pathName);
            AddArtistsToRepository(artists);
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
            MenuHelper.AddSeparator();
        }
    }

    protected override void RemoveItem()
    {
        try
        {
            RemoveArtistFromRepository();
        }
        catch (FormatException fe)
        {
            MenuHelper.AddSeparator();
            Console.WriteLine(fe.Message);
        }
        catch (ArgumentException ae)
        {
            MenuHelper.AddSeparator();
            Console.WriteLine(ae.Message);
        }
        finally
        {
            MenuHelper.AddSeparator();
        }
    }

    private void RemoveArtistFromRepository()
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
        _repository.Save();

        MenuHelper.AddSeparator();
        Console.WriteLine($"INFO : Artist '{artist.FirstName} {artist.LastName}' removed succesfully!\n\tChanges saved!");
    }

    protected override void AddNewItem()
    {
        try
        {
            AddNewArtistToRepository();
        }
        catch (ArgumentException ae)
        {
            MenuHelper.AddSeparator();
            Console.WriteLine(ae.Message);
        }
        finally
        {
            MenuHelper.AddSeparator();
        }
    }

    private void AddNewArtistToRepository()
    {
        MenuHelper.AddSeparator();
        Console.WriteLine("Add new artist:");
        Console.Write("\tFirstName: ");
        var firstName = Console.ReadLine()!.Trim();
        firstName = ConvertToPascalFormat(firstName);

        Console.Write("\tLastName: ");
        var lastName = Console.ReadLine()!.Trim();
        lastName = ConvertToPascalFormat(lastName);

        if (_repository.GetAll().Where(x => x.FirstName == firstName && x.LastName == lastName).Any())
        {
            throw new ArgumentException("ERROR : Artist exists! You can not add same artist!");
        }

        _repository.Add(new Artist { FirstName = firstName, LastName = lastName });
        _repository.Save();

        MenuHelper.AddSeparator();
        Console.WriteLine($"INFO : Artist '{firstName} {lastName}' added succesfully!\n\tChanges saved!");
    }
}
