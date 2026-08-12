using MvcMovie.Models;
using MvcMovie.Repos;


namespace MvcMovie.Services
{
    public class GenreService : IGenreService
    {
        private readonly GenreRepo _genreRepo;

        public GenreService(GenreRepo genreRepo)
        {
            _genreRepo = genreRepo;
        }

        public async Task<List<Genre>> ObtenerGeneros()
        {
            List<Genre> genres;
            try
            {
                genres = await _genreRepo.GetGenres();
                return genres;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
