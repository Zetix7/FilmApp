using FilmApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmApp.Data;

public class FilmAppDbContext : DbContext
{
    public FilmAppDbContext(DbContextOptions<FilmAppDbContext> options) : base(options)
    {
    }

    public DbSet<Artist> Artists { get; set; }
    public DbSet<Movie> Movies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=FilmAppStorage;Integrated Security=True;Encrypt=False");
    }
}
