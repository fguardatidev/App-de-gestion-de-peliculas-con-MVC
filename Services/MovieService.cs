using MvcMovie.Models;
using MvcMovie.Repos;

namespace MvcMovie.Services
{
    public class MovieService : IMovieService
    {
        private readonly MovieRepo movieRepo;

        public MovieService(MovieRepo _movieRepo)
        {
            movieRepo = _movieRepo;
        }

        public Task<MovieGenreViewModel> ObtenerPeliculas(string movieGenre, string searchString)
        {
            Task<MovieGenreViewModel> movieGenreViewModel;

            if (movieRepo.IsContextNull())
            {
                throw new Exception("Entity set 'MvcMovieContext.Movie'  is null.");
            }
            
            try
            {
                movieGenreViewModel = movieRepo.GetMoviesByGenreOrTitle(movieGenre, searchString);
            }
            
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return movieGenreViewModel;
        }

    }
}
