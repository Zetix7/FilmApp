using FilmApp.Data.Entities;
using FilmApp.Data.Repositories;

namespace FilmApp.Components.Menu;

public abstract class Menu<T> where T : class, IEntity
{
    private readonly IRepository<T> _repository;

    public Menu(IRepository<T> repository)
    {
        _repository = repository;
    }

    protected abstract void RemoveItem();

    protected abstract void AddNewItem();

    protected void SaveChangesInDatabase()
    {
        _repository.Save();
        AddSeparator();
        Console.WriteLine("\tChanges saved!");
        AddSeparator();
    }
    protected void ReadItemById()
    {
        try
        {
            AddSeparator();
            Console.Write("\tChoose one ID: ");
            var choise = Console.ReadLine()!.Trim();
            if (!int.TryParse(choise, out int id))
            {
                throw new FormatException($"ERROR : Invalid format! Insert MUST be a digit!");
            }

            var item = _repository.GetById(id);
            if (item == null)
            {
                throw new ArgumentException($"ERROR : Invalid value! ID not exists! Try again!");
            }
            AddSeparator();
            Console.WriteLine($"{typeof(T).Name} : {item}");
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

    protected void ReadAllItems()
    {
        AddSeparator();
        if(_repository.GetAll().Count() == 0)
        {
            Console.WriteLine("INFO : Database is empty!");
        }

        foreach (var item in _repository.GetAll())
        {
            Console.WriteLine(item);
        }
        
        AddSeparator();
    }

    protected void AddSeparator()
    {
        Console.WriteLine("_________________________________________________________________________________________________________________");
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
            result += word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower() + " ";
        }

        return result.Trim();
    }
}
