using MvcMovie.Models;
namespace MvcMovie.Services
{
    public interface IGenreService
    {
        public Task<List<Genre>> ObtenerGeneros();

        public Task<Genre?> ObtenerDetalles(int? id);

        public Task<Genre> CrearGenero(Genre genre);

        public Task<Genre?> EditarGenero(int? id, Genre genre);

        public Task<Genre?> EliminarGenero(int? id);
    }
}
