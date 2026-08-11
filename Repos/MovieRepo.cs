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

        public async Task<MovieGenreViewModel> GetMoviesByGenreOrTitle(string movieGenre, string searchString)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Genre
                                            orderby m.Name
                                            select m.Name;
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

            var movieGenreVM = new MovieGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Movies = await movies.ToListAsync()
            };

            return movieGenreVM;
        }
    }
}
