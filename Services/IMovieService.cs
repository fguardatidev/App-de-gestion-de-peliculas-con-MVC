using MvcMovie.Models;

namespace MvcMovie.Services
{
    public interface IMovieService
    {
        public Task<MovieGenreViewModel> ObtenerPeliculas(string movieGenre, string searchString);

    }
}
