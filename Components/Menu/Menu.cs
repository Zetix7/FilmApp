using FilmApp.Data.Entities;
using FilmApp.Data.Repositories;
using FilmApp.Components.Menu.Extensions;

namespace FilmApp.Components.Menu;

public abstract class Menu<T> : IMenu<T> where T : class, IEntity
{
    private readonly IRepository<T> _repository;

    public Menu(IRepository<T> repository)
    {
        _repository = repository;
    }

    public void LoadMenu()
    {
        Console.WriteLine($"{ConvertToPascalFormat(typeof(T).Name)} - What do you want to do?");
        Console.WriteLine("\tChoose one option:");
        Console.WriteLine($"\t\t1 - Read all {typeof(T).Name.ToLower()}s");
        Console.WriteLine($"\t\t2 - Read {typeof(T).Name.ToLower()} by ID");
        Console.WriteLine($"\t\t3 - Add new {typeof(T).Name.ToLower()}");
        Console.WriteLine($"\t\t4 - Remove {typeof(T).Name.ToLower()}");
        Console.WriteLine($"\t\t5 - Read from {typeof(T).Name.ToLower()}s.csv file");
        Console.WriteLine($"\t\t6 - Save to {typeof(T).Name.ToLower()}s.csv file");
        Console.WriteLine($"\t\t7 - Read from {typeof(T).Name.ToLower()}s.xml file");
        Console.WriteLine($"\t\t8 - Save to {typeof(T).Name.ToLower()}s.xml file");
        Console.WriteLine("\t\tQ - Return");
        Console.Write("\tYour choise: ");
    }

    protected abstract void RemoveItem();

    protected abstract void AddNewItem();

    protected void ReadItemById()
    {
        try
        {
            if (!_repository.GetAll().Any())
            {
                throw new IndexOutOfRangeException("INFO : No data to read!");
            }

            MenuHelper.AddSeparator();
            Console.Write("\tChoose one ID: ");
            var choise = Console.ReadLine()!.Trim();

            if (!int.TryParse(choise, out int id))
            {
                throw new FormatException($"ERROR : Invalid format! Insert MUST be a digit!");
            }

            var item = _repository.GetById(id) ?? throw new ArgumentException($"ERROR : Invalid value! ID not exists! Try again!");
            MenuHelper.AddSeparator();
            Console.WriteLine($"{typeof(T).Name} : {item}");
        }
        catch (IndexOutOfRangeException ie)
        {
            MenuHelper.AddSeparator();
            Console.WriteLine(ie.Message);
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

    protected void ReadAllItems()
    {
        MenuHelper.AddSeparator();
        if (!_repository.GetAll().Any())
        {
            Console.WriteLine("INFO : No data to read!");
        }

        foreach (var item in _repository.GetAll())
        {
            Console.WriteLine(item);
        }

        MenuHelper.AddSeparator();
    }

    protected string ConvertToPascalFormat(string input)
    {
        if (input.Length < 1 || string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException("ERROR : Invalid value! Input is empty or contains only spaces!");
        }

        input = input.Trim();
        var words = input.Split(' ');
        var result = "";

        foreach (var word in words)
        {
            if (word.Equals(""))
            {
                continue;
            }
            result += word[..1].ToUpper() + word[1..].ToLower() + " ";
        }

        return result.Trim();
    }
}
