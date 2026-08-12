using MvcMovie.Models;
using MvcMovie.Repos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcMovie.Services
{
    public class MovieService : IMovieService
    {
        private readonly MovieRepo movieRepo;
        private readonly GenreRepo genreRepo;

        public MovieService(MovieRepo _movieRepo, GenreRepo _genreRepo)
        {
            movieRepo = _movieRepo;
            genreRepo = _genreRepo;
        }

        public async Task<MovieGenreViewModel> ObtenerPeliculas(string movieGenre, string searchString)
        {
            List<Movie> movies;
            SelectList genres;

            if (movieRepo.IsContextNull())
            {
                throw new Exception("Entity set 'MvcMovieContext.Movie'  is null.");
            }
            
            try
            {
                movies = await movieRepo.GetMoviesByGenreOrTitle(movieGenre, searchString); //obtengo las peliculas
                genres = new SelectList(await genreRepo.GetGenresString()); //obtengo los generos
            }
            
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            var movieGenreViewModel = new MovieGenreViewModel //creo el view model con los datos obtenidos
            {
                Movies = movies,
                Genres = genres
            };

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
