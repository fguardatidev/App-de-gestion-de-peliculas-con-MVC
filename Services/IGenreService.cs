using MvcMovie.Models;
namespace MvcMovie.Services
{
    public interface IGenreService
    {
        public Task<List<Genre>> ObtenerGeneros();
    }
}
