using FilmApp.Data;
using FilmApp.Services;

namespace FilmApp;

public class App : IApp
{
    private readonly FilmAppDbContext _filmAppDbContext;
    private readonly IUserCommunication _userCommunication;

    public App(
        FilmAppDbContext filmAppDbContext,
        IUserCommunication userCommunication)
    {
        _filmAppDbContext = filmAppDbContext;
        _filmAppDbContext.Database.EnsureCreated();
        _userCommunication = userCommunication;
    }

    public void Run()
    {
        _userCommunication.WhatToDo();
    }
}
