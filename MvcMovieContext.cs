using Microsoft.EntityFrameworkCore;

public class MvcMovieContext(DbContextOptions<MvcMovieContext> options) : DbContext(options)
{
    public DbSet<MvcMovie.Models.Movie> Movie { get; set; } = default!;
    public DbSet<MvcMovie.Models.Genre> Genre { get; set; } = default!;

}
