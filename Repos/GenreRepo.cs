using Microsoft.AspNetCore.Mvc.Rendering;
using MvcMovie.Models;
using Microsoft.EntityFrameworkCore;

namespace MvcMovie.Repos
{
    public class GenreRepo
    {
        public readonly MvcMovieContext _context;

        public GenreRepo(MvcMovieContext context)
        {
            _context = context;
        }

        public async Task<List<Genre>> GetGenres()
        {
            return await _context.Genre.ToListAsync();
        }

        public async Task<List<String>> GetGenresString()
        {
            IQueryable<string> genreQuery =  from m in _context.Genre
                   orderby m.Name
                   select m.Name;

            var genres = await genreQuery.Distinct().ToListAsync();
            return genres;
        }
    }
}
