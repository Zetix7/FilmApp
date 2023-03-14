using FilmApp.Components.FileCreator;
using FilmApp.Data.Entities;
using FilmApp.Data.Repositories;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace FilmApp.Components.Menu;

internal class MovieMenu : Menu<Movie>, IMenu<Movie>
{
    private readonly IRepository<Movie> _repository;
    private readonly ICsvFile _csvFile;
    private readonly IXmlFile _xmlFile;

    public MovieMenu(IRepository<Movie> repository, ICsvFile csvFile, IXmlFile xmlFile) : base(repository)
    {
        _repository = repository;
        _csvFile = csvFile;
        _xmlFile = xmlFile;
    }
    public void LoadMenu()
    {
        while (true)
        {
            Console.WriteLine("Movies - What do you want to do?");
            Console.WriteLine("\tChoose one option:");
            Console.WriteLine("\t\t1 - Read all movies");
            Console.WriteLine("\t\t2 - Read movie by ID");
            Console.WriteLine("\t\t3 - Add new movie");
            Console.WriteLine("\t\t4 - Remove movie");
            Console.WriteLine("\t\t5 - Save changes");
            Console.WriteLine("\t\t6 - Read from movies.csv file");
            Console.WriteLine("\t\t7 - Save to movies.csv file");
            Console.WriteLine("\t\t8 - Read from movies.xml file");
            Console.WriteLine("\t\t9 - Save to movies.xml file");
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
                ReadMovieFromCsvFile(@"Resources\Files\movies.csv");
            }
            else if (choise == "7")
            {
                SaveMoviesToCsvFile();
            }
            else if (choise == "8")
            {
                ReadMovieFromXmlFile(@"Resources\Files\movies.xml");
            }
            else if (choise == "9")
            {
                SaveMoviesToXmlFile();
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

    private void SaveMoviesToXmlFile()
    {
        var movies = _repository.GetAll().ToList();

        AddSeparator();
        if (movies.Count > 0)
        {
            var data = new XElement("Movies", movies
                .Select(x => new XElement("Movie",
                    new XAttribute("Title", x.Title),
                    new XAttribute("Year", x.Year),
                    new XAttribute("Universe", x.Universe),
                    new XAttribute("BoxOffice", x.BoxOffice))));

            var xmlFile = new XDocument(data);
            xmlFile.Save(@"Resources\Files\movies.xml");

            Console.WriteLine("INFO : Data from databese succesfully saved to movies.xml file!");
        }
        else
        {
            Console.WriteLine("INFO : Database is empty!");
        }
        AddSeparator();
    }

    private void ReadMovieFromXmlFile(string pathName)
    {
        try
        {
            AddSeparator();
            if (!File.Exists(pathName))
            {
                throw new FileNotFoundException($"ERROR : Directory or file '{pathName}' not exists!");
            }

            var movies = _xmlFile.ReadMovieXmlFile(pathName);
            ReadMovies(movies);
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

    private void SaveMoviesToCsvFile()
    {
        var movies = _repository.GetAll().ToList();

        AddSeparator();
        if (movies.Count > 0)
        {
            var data = new StringBuilder();
            foreach (var movie in movies)
            {
                data.AppendLine($"{movie.Title},{movie.Year},{movie.Universe},{movie.BoxOffice}\n");
            }
            File.AppendAllText(@"Resources\Files\movies.csv", data.ToString());

            Console.WriteLine("INFO : Data from databese succesfully saved to movies.csv file!");
        }
        else
        {
            Console.WriteLine("INFO : Database is empty!");
        }
        AddSeparator();
    }

    private void ReadMovieFromCsvFile(string pathName)
    {
        try
        {
            AddSeparator();
            if (!File.Exists(pathName))
            {
                throw new FileNotFoundException($"ERROR : Directory or file '{pathName}' not exists!");
            }

            var movies = _csvFile.ReadMovieCsvFile(pathName);
            ReadMovies(movies);
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

    private void ReadMovies(List<FileCreator.Models.Movie> movies)
    {
        var count = 0;
        foreach (var movie in movies)
        {
            if (_repository.GetAll().Where(x => x.Title == movie.Title && x.Universe == movie.Universe).Any())
            {
                continue;
            }
            _repository.Add(new Movie
            {
                Title = movie.Title,
                Year = movie.Year,
                Universe = movie.Universe,
                BoxOffice = movie.BoxOffice
            });
            count++;
            Console.WriteLine($"\t{movie.Title}, {movie.Year}, {movie.Universe}, {movie.BoxOffice}");
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

    protected override void AddNewItem()
    {
        try
        {
            AddSeparator();
            Console.WriteLine("Add new artist:");
            Console.Write("\tTitle: ");
            var title = Console.ReadLine()!.Trim();
            title = PascalFormat(title);

            Console.Write("\tYear: ");
            var year = Console.ReadLine()!.Trim();
            if (!int.TryParse(year, out int releaseYear))
            {
                throw new FormatException("ERROR : Invalid format!\n\t\tValue must be integer!\n\tOr you stuck here for long time!");
            }

            Console.Write("\tUniverse: ");
            var universe = Console.ReadLine()!.Trim();
            universe = PascalFormat(universe);

            Console.Write("\tBoxOffice: ");
            var boxOffice = Console.ReadLine()!.Trim();
            if (!decimal.TryParse(boxOffice, out decimal profits))
            {
                throw new FormatException("ERROR : Invalid format!\n\t\tValue must be digit!\n\tOr you stuck here for long time!");
            }

            if (_repository.GetAll().Where(x => x.Title == title).Any())
            {
                throw new ArgumentException("ERROR : Movie exists in database! You can not add same movie!");
            }

            _repository.Add(new Movie { Title = title, Year = releaseYear, Universe = universe, BoxOffice = profits });

            AddSeparator();
            Console.WriteLine($"INFO :\tMovie {title} added succesfully! Do not forget save changes!");
        }
        catch (ArgumentException ae)
        {
            AddSeparator();
            Console.WriteLine(ae.Message);
        }
        catch (FormatException fe)
        {
            AddSeparator();
            Console.WriteLine(fe.Message);
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

            var movie = _repository.GetById(id) ?? throw new ArgumentException($"ERROR : Invalid value! ID not exists! Try again!");
            _repository.Remove(movie!);

            AddSeparator();
            Console.WriteLine($"INFO :\tMovie {movie.Title} from {movie.Universe} universe removed succesfully!\n\tDo not forget save changes!");
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
}