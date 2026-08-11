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

        public async Task<Movie?> ObtenerDetalles(int? id)
        {
            if(id == null)
            {
                return null;
            }
            try
            {
                var movie = await movieRepo.GetMovieByID((int)id);
                return movie != null ? movie : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Movie> CrearPelicula(Movie movie)
        {
            return await movieRepo.CreateMovie(movie);
        }

        public async Task<Movie?> EliminarPelicula(int? id)
        {
            var movie = await ObtenerDetalles(id);

            if(movie != null)
            {
                await movieRepo.DeleteMovie(movie);
                return movie;
            }

            return null;
        }
    }
}
