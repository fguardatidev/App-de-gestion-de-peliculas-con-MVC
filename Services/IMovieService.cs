using MvcMovie.Models;

namespace MvcMovie.Services
{
    public interface IMovieService
    {
        public Task<MovieGenreViewModel> ObtenerPeliculas(string movieGenre, string searchString);

        public Task<Movie?> ObtenerDetalles(int? id);

        public Task<Movie> CrearPelicula(Movie movie);

        public Task<Movie?> EliminarPelicula(int? id);

    }
}
