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

        public async Task<bool> GenreExists(int? id)
        {
            return _context.Genre.Any(e => e.Id == id);
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

        public async Task<Genre?> FindGenre(int id)
        {
            try
            {
                var genre = await _context.Genre
                                  .FirstOrDefaultAsync(m => m.Id == id);
                return genre;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Genre> CreateGenre(Genre genre)
        {
            try 
            { 
                _context.Add(genre);
                await _context.SaveChangesAsync();
                return genre;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Genre> EditGenre(Genre genre)
        {
            try
            {
                _context.Update(genre);
                await _context.SaveChangesAsync();
                return genre;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Genre> DeleteGenre(Genre genre)
        {
            try
            {
                _context.Genre.Remove(genre);
                await _context.SaveChangesAsync();
                return genre;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
