using FilmApp.Components.Menu;
using FilmApp.Data.Entities;

namespace FilmApp.Services;

public class UserCommunication : IUserCommunication
{
    private readonly IMenu<Artist> _artistMenu;
    private readonly IMenu<Movie> _movieMenu;

    public UserCommunication(IMenu<Artist> artistMenu, IMenu<Movie> movieMenu)
    {
        _artistMenu = artistMenu;
        _movieMenu = movieMenu;
    }

    public void WhatToDo()
    {
        Console.WriteLine("Film App\n");

        while (true)
        {
            Console.WriteLine("You can Read, Add, Remove and Save data.");
            Console.WriteLine("\tChoose one option:");
            Console.WriteLine("\t\t1 - Artists - Read, Add, Remove or Save");
            Console.WriteLine("\t\t2 - Movies - Read, Add, Remove or Save");
            Console.WriteLine("\t\tQ - Exit");
            Console.Write("\tYour choise: ");
            var choise = Console.ReadLine()!.ToUpper();

            if (choise == "1")
            {
                AddSeparator();
                _artistMenu.LoadMenu();
            }
            else if (choise == "2")
            {
                AddSeparator();
                _movieMenu.LoadMenu();
            }
            else if (choise == "Q")
            {
                break;
            }
            else
            {
                AddSeparator();
                Console.WriteLine("ERROR : Choose one option: 1 or 2 or Q!\n\tIf not, You'll stuck here forever!");
                AddSeparator();
            }
        }
    }

    private void AddSeparator()
    {
        Console.WriteLine("_________________________________________________________________________________________________________________");
    }
}
