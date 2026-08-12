using Microsoft.AspNetCore.Mvc.Rendering;
using MvcMovie.Models;
using Microsoft.EntityFrameworkCore;

namespace MvcMovie.Repos
{
    public class MovieRepo
    {
        private readonly MvcMovieContext _context;

        public MovieRepo(MvcMovieContext context)
        {
            _context = context;
        }

        public bool IsContextNull()
        {
            return _context.Movie == null;
        }


        //separar la logica de obtener las peliculas y los generos, en mi opinion deberian fusionarse en el service
        public async Task<List<Movie>> GetMoviesByGenreOrTitle(string movieGenre, string searchString)
        {
            /*
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Genre
                                            orderby m.Name
                                            select m.Name;
            */
            var movies = from m in _context.Movie
                         select m;
            

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title!.ToUpper().Contains(searchString.ToUpper()));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre.Name == movieGenre);
            }

            /*
            var movieGenreVM = new MovieGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Movies = await movies.ToListAsync()
            };
            */

            return await movies.ToListAsync();
        }
        
        public async Task<Movie?> GetMovieByID(int id) //id != null debe verificarse en el metodo del servicio
        {
            try
            {
                var movie = await _context.Movie
                                  .FirstOrDefaultAsync(m => m.Id == id);
                return movie != null ? movie : null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Movie> CreateMovie(Movie movie)
        {
            try
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return movie;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Movie?> DeleteMovie(Movie movie)
        {
           try
           {
              _context.Movie.Remove(movie);
              await _context.SaveChangesAsync();
              return movie;
           }
           catch (Exception ex)
           {
              throw new Exception(ex.Message);
           }
        }
    }
}
