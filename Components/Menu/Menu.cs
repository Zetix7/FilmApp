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
        Console.WriteLine($"{PascalFormat(typeof(T).Name)} - What do you want to do?");
        Console.WriteLine("\tChoose one option:");
        Console.WriteLine($"\t\t1 - Read all {typeof(T).Name.ToLower()}s");
        Console.WriteLine($"\t\t2 - Read {typeof(T).Name.ToLower()} by ID");
        Console.WriteLine($"\t\t3 - Add new {typeof(T).Name.ToLower()}");
        Console.WriteLine($"\t\t4 - Remove {typeof(T).Name.ToLower()}");
        Console.WriteLine("\t\t5 - Save changes");
        Console.WriteLine($"\t\t6 - Read from {typeof(T).Name.ToLower()}s.csv file");
        Console.WriteLine($"\t\t7 - Save to {typeof(T).Name.ToLower()}s.csv file");
        Console.WriteLine($"\t\t8 - Read from {typeof(T).Name.ToLower()}s.xml file");
        Console.WriteLine($"\t\t9 - Save to {typeof(T).Name.ToLower()}s.xml file");
        Console.WriteLine("\t\tQ - Exit");
        Console.Write("\tYour choise: ");
    }

    protected abstract void RemoveItem();

    protected abstract void AddNewItem();

    protected void SaveChangesInDatabase()
    {
        _repository.Save();
        MenuHelper.AddSeparator();
        Console.WriteLine("\tChanges saved!");
        MenuHelper.AddSeparator();
    }
    protected void ReadItemById()
    {
        try
        {
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
        if(!_repository.GetAll().Any())
        {
            Console.WriteLine("INFO : Database is empty!");
        }

        foreach (var item in _repository.GetAll())
        {
            Console.WriteLine(item);
        }
        
        MenuHelper.AddSeparator();
    }

    protected string PascalFormat(string insert)
    {
        if (insert.Length < 1 || string.IsNullOrWhiteSpace(insert))
        {
            throw new ArgumentException("ERROR : Invalid value! Insert is empty or contains only spaces!");
        }

        insert = insert.Trim();
        var words = insert.Split(' ');
        var result = "";

        foreach (var word in words)
        {
            if (word.Equals(""))
            {
                continue;
            }
            //result += word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower() + " ";
            result += word[..1].ToUpper() + word[1..].ToLower() + " ";
        }

        return result.Trim();
    }
}
