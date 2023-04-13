using FilmApp;
using FilmApp.Components.DataProvider;
using FilmApp.Components.FileCreator;
using FilmApp.Components.Menu;
using FilmApp.Data;
using FilmApp.Data.Entities;
using FilmApp.Data.Repositories;
using FilmApp.Services;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IRepository<Artist>, ListRepository<Artist>>();
services.AddSingleton<IRepository<Movie>, ListRepository<Movie>>();
services.AddSingleton<IDataProvider, DataGenerator>();
services.AddSingleton<ICsvFile, CsvFile>();
services.AddSingleton<IXmlFile, XmlFile>();
services.AddSingleton<IRepository<Artist>, SqlRepository<Artist>>();
services.AddSingleton<IRepository<Movie>, SqlRepository<Movie>>();
services.AddDbContext<FilmAppDbContext>();
services.AddSingleton<IUserCommunication, UserCommunication>();
services.AddSingleton<IMenu<Artist>, ArtistMenu>();
services.AddSingleton<IMenu<Movie>, MovieMenu>();

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;
app.Run();
