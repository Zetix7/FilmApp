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
}
