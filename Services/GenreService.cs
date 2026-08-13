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

        public async Task<Genre?> ObtenerDetalles(int? id)
        {
            if(id == null)
            {
                return null;
            }

            try
            {
                var genre = await _genreRepo.FindGenre((int)id);
                return genre;
            }

            catch (Exception ex)
            {
                throw new Exception("Error al obtener el genero: " + ex.Message);
            }
        }

        public async Task<Genre> CrearGenero(Genre genre)
        {
            try
            {
                await _genreRepo.CreateGenre(genre);
                return genre;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el genero: " + ex.Message);
            }
        }

        public async Task<Genre?> EditarGenero(int? id, Genre genre)
        {
            if(id == null || id != genre.Id)
            {
                return null;
            }

            if(!(await _genreRepo.GenreExists(id)))
            {
                return null;
            }

            try
            {
                await _genreRepo.EditGenre(genre);
                return genre;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar el genero: " + ex.Message);
            }

        }

        public async Task<Genre?> EliminarGenero(int? id)
        {
            var genre = await ObtenerDetalles(id);
            if (genre == null)
            {
                return null;
            }

            try
            {
                await _genreRepo.DeleteGenre(genre);
                return genre;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el genero: " + ex.Message);
            }
        }
    }
}
