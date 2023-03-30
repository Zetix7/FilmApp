using FilmApp.Data.Entities;

namespace FilmApp.Components.DataProvider;

public class DataGenerator : IDataProvider
{
    public List<Artist> GenerateSampleArtists()
    {
        return new List<Artist> {
            new Artist { FirstName = "Robert", LastName = "Downey Jr." },
            new Artist { FirstName = "Gweneth", LastName = "Paltrow" },
            new Artist { FirstName = "Samuel L.", LastName = "Jackson" },
            new Artist { FirstName = "Chris", LastName = "Hemsworth" },
            new Artist { FirstName = "Chris", LastName = "Evans" },
            new Artist { FirstName = "Chris", LastName = "Pratt" },
            new Artist { FirstName = "Hayley", LastName = "Atwell" },
            new Artist { FirstName = "Vin", LastName = "Diesel" },
            new Artist { FirstName = "Emily", LastName = "Van Camp" },
            new Artist { FirstName = "Michelle", LastName = "Rodriguez" },
            new Artist { FirstName = "Elsa", LastName = "Pataky" },
            new Artist { FirstName = "Dwayne", LastName = "Johnson" },
            new Artist { FirstName = "Cole", LastName = "Hauser" },
            new Artist { FirstName = "Eva", LastName = "Mendes" },
            new Artist { FirstName = "Daniel", LastName = "Raddclife" },
            new Artist { FirstName = "Emma", LastName = "Watson" },
            new Artist { FirstName = "Rupert", LastName = "Grint" },
            new Artist { FirstName = "Richard", LastName = "Harris" },
            new Artist { FirstName = "Tom", LastName = "Holland" },
            new Artist { FirstName = "Bradley", LastName = "Cooper" },
            new Artist { FirstName = "Karen", LastName = "Gillan" },
            new Artist { FirstName = "Natalie", LastName = "Portman" },
            new Artist { FirstName = "Jeremy", LastName = "Renner" },
            new Artist { FirstName = "Tom", LastName = "Hiddleston" },
            new Artist { FirstName = "Sebastian", LastName = "Stan" },
            new Artist { FirstName = "Scarlett", LastName = "Johansson" },
            new Artist { FirstName = "Brie", LastName = "Larson" },
            new Artist { FirstName = "Elizabeth", LastName = "Olsen" },
            new Artist { FirstName = "Jason", LastName = "Statham" },
            new Artist { FirstName = "Paul", LastName = "Walker" },
            new Artist { FirstName = "Jordana", LastName = "Brewster" },
            new Artist { FirstName = "Tyrese", LastName = "Gibson" },
            new Artist { FirstName = "Lucas", LastName = "Black" },
            new Artist { FirstName = "Sung", LastName = "Kang" },
            new Artist { FirstName = "Gal", LastName = "Gadot" },
        };
    }

    public List<Movie> GenerateSampleMovies()
    {
        return new List<Movie>
        {
            new Movie { Title = "Iron Man", Year = 2008, Universe = "Marvel", BoxOffice = 586_000_000.07M },
            new Movie { Title = "Incredible Hulk", Year = 2008, Universe = "Marvel", BoxOffice = 264_000_000M },
            new Movie { Title = "Iron Man 2", Year = 2010, Universe = "Marvel", BoxOffice = 625_000_000.00M },
            new Movie { Title = "Thor", Year = 2011, Universe = "Marvel", BoxOffice = 451_000_000.00M },
            new Movie { Title = "Captain America: The First Avenger", Year = 2011, Universe = "Marvel", BoxOffice = 371_000_000.00M },
            new Movie { Title = "Avengers", Year = 2012, Universe = "Marvel", BoxOffice = 1_521_000_000.00M },
            new Movie { Title = "Iron Man 2", Year = 2013, Universe = "Marvel", BoxOffice = 1_219_000_000.00M },
            new Movie { Title = "Thor: The Dark World", Year = 2013, Universe = "Marvel", BoxOffice = 648_000_000.00M },
            new Movie { Title = "Captain America: The Winter Soldier", Year = 2014, Universe = "Marvel", BoxOffice = 716_000_000.00M },
            new Movie { Title = "Guardians of the Galaxy", Year = 2014, Universe = "Marvel", BoxOffice = 776_000_000.00M },
            new Movie { Title = "Avengers: Age of Ultron", Year = 2015, Universe = "Marvel", BoxOffice = 1_409_000_000.00M },
            new Movie { Title = "Ant-Man", Year = 2015, Universe = "Marvel", BoxOffice = 520_500_000.00M },
            new Movie { Title = "Captain America: Civil War", Year = 2016, Universe = "Marvel", BoxOffice = 1_155_800_000.00M },
            new Movie { Title = "Doctor Strange", Year = 2016, Universe = "Marvel", BoxOffice = 680_200_000.00M },
            new Movie { Title = "Guardians of the Galaxy vol. 2", Year = 2017, Universe = "Marvel", BoxOffice = 867_000_000.00M },
            new Movie { Title = "Spider-Man: Homecoming", Year = 2017, Universe = "Marvel", BoxOffice = 882_900_000.00M },
            new Movie { Title = "Thor: Ragnarok", Year = 2017, Universe = "Marvel", BoxOffice = 858_000_000.00M },
            new Movie { Title = "Black Panther", Year = 2018, Universe = "Marvel", BoxOffice = 1_350_500_000.00M },
            new Movie { Title = "Avengers: Infinity War", Year = 2018, Universe = "Marvel", BoxOffice = 2_055_700_000.00M },
            new Movie { Title = "Ant-Man and the Wasp", Year = 2018, Universe = "Marvel", BoxOffice = 520_500_000.00M },
            new Movie { Title = "Captain Marvel", Year = 2019, Universe = "Marvel", BoxOffice = 1_132_800_000.00M },
            new Movie { Title = "Avengers: Endgame", Year = 2019, Universe = "Marvel", BoxOffice = 2_808_300_000.00M },
            new Movie { Title = "Spider-Man Far From Home", Year = 2019, Universe = "Marvel", BoxOffice = 1_136_300_000.00M },
            new Movie { Title = "Black Widow", Year = 2021, Universe = "Marvel", BoxOffice = 382_400_000.00M },
            new Movie { Title = "Shang-Chi and the legend of Ten Rings", Year = 2021, Universe = "Marvel", BoxOffice = 434_400_000.00M },
            new Movie { Title = "Eternals", Year = 2021, Universe = "Marvel", BoxOffice = 403_200_000.00M },
            new Movie { Title = "Spider-Man No Way Home", Year = 2021, Universe = "Marvel", BoxOffice = 1_910_600_000.00M },
            new Movie { Title = "Doctor Strange in the Multiverse of Madness", Year = 2022, Universe = "Marvel", BoxOffice = 959_000_000.00M },

            new Movie { Title = "The Fast and the Furious", Year = 2001, Universe = "Fast adn Furious", BoxOffice = 206_400_000.00M },
            new Movie { Title = "2 Fast 2 Furious", Year = 2003, Universe = "Fast adn Furious", BoxOffice = 236_400_000.00M },
            new Movie { Title = "The Fast and the Furious: Tokyo Drift", Year = 2006, Universe = "Fast adn Furious", BoxOffice = 157_800_000.00M },
            new Movie { Title = "Fast & Furious", Year = 2009, Universe = "Fast adn Furious", BoxOffice = 359_300_000.00M },
            new Movie { Title = "Fast Five", Year = 2011, Universe = "Fast adn Furious", BoxOffice = 630_100_000.00M },
            new Movie { Title = "Fast and Furious 6", Year = 2013, Universe = "Fast adn Furious", BoxOffice = 789_300_000.00M },
            new Movie { Title = "Furious 7", Year = 2015, Universe = "Fast adn Furious", BoxOffice = 1_514_500_000.00M },
            new Movie { Title = "The Fate of the Furious", Year = 2017, Universe = "Fast adn Furious", BoxOffice = 1_236_700_000.00M },
            new Movie { Title = "Fast & Furious Presents: Hoobs and Show", Year = 2019, Universe = "Fast adn Furious", BoxOffice = 760_700_000.00M },
            new Movie { Title = "F9: The Fast Saga", Year = 2021, Universe = "Fast adn Furious", BoxOffice = 720_700_000.00M },

            new Movie { Title = "Harry Potter and the Philosophers Stone", Year = 2001, Universe = "Harry Potter", BoxOffice = 965_000_000.00M },
            new Movie { Title = "Harry Potter and the Chember of Secrets", Year = 2002, Universe = "Harry Potter", BoxOffice = 874_900_000.00M },
            new Movie { Title = "Harry Potter and the Prisoner of Azkaban", Year = 2004, Universe = "Harry Potter", BoxOffice = 789_600_000.00M },
            new Movie { Title = "Harry Potter and the Goblet of Fire", Year = 2005, Universe = "Harry Potter", BoxOffice = 886_700_000.00M },
            new Movie { Title = "Harry Potter and the Order of the Phoenix", Year = 2007, Universe = "Harry Potter", BoxOffice = 939_600_000.00M },
            new Movie { Title = "Harry Potter and the Half-Blood Prince", Year = 2009, Universe = "Harry Potter", BoxOffice = 929_400_000.00M },
            new Movie { Title = "Harry Potter and the Deathly Hallows Part I", Year = 2010, Universe = "Harry Potter", BoxOffice = 951_700_000.00M },
            new Movie { Title = "Harry Potter and the Deathly Hallows Part II", Year = 2011, Universe = "Harry Potter", BoxOffice = 1_316_300_000.00M },
            new Movie { Title = "Fantastic Beasts and Where to Find Them", Year = 2016, Universe = "Harry Potter", BoxOffice = 811_700_000.00M },
            new Movie { Title = "Fantastic Beasts: The Crimes of Grindelwald", Year = 2018, Universe = "Harry Potter", BoxOffice = 648_400_000.00M },
            new Movie { Title = "Fantastic Beasts: The Secrets of Dumbledore", Year = 2020, Universe = "Harry Potter", BoxOffice = 404_500_000.00M },
        };
    }
}
