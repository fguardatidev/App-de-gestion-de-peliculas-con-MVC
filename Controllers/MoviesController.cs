
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;
using MvcMovie.Services;

public class MoviesController : Controller
{
    private readonly MvcMovieContext _context;
    private readonly IMovieService _movieService;
    private readonly IGenreService _genreService;

    public MoviesController(MvcMovieContext context, IMovieService movieService, IGenreService genreService)
    {
        _context = context;
        _movieService = movieService;
        _genreService = genreService;
    }

    // GET: MOVIES
    public async Task<IActionResult> Index(string movieGenre, string searchString)
    {

        var movieGenreVM = await _movieService.ObtenerPeliculas(movieGenre, searchString);

        return View(movieGenreVM);
    }

    // GET: MOVIES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        var movie = await _movieService.ObtenerDetalles(id);

        return movie == null ? NotFound() : View(movie);
    }

    // GET: MOVIES/Create
    public async Task<IActionResult> Create()
    {
        var genres = await _genreService.ObtenerGeneros();

        ViewBag.Genres = new SelectList(genres, "Id", "Name");

        return View();
    }

    // POST: MOVIES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,GenreId,Price,Rating,Director,Duration,Seen,PersonalRating")] Movie movie)
    {
        if (ModelState.IsValid)
        {
            await _movieService.CrearPelicula(movie);
            return RedirectToAction(nameof(Index));
        }
        return View(movie);
    }

    // GET: MOVIES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {

        var genres = await _genreService.ObtenerGeneros();
        var movie = await _movieService.ObtenerDetalles(id);

        ViewBag.Genres = new SelectList(genres, "Id", "Name");

        return movie == null ? NotFound() : View(movie);
    }

    // POST: MOVIES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Title,ReleaseDate,GenreId,Price,Rating,Director,Duration,Seen,PersonalRating")] Movie movie)
    {
        if(!ModelState.IsValid)
        {
            return View(movie);
        }

        try
        {
            await _movieService.EditarPelicula(id, movie);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return RedirectToAction(nameof(Index));
    }

    // GET: MOVIES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        var movie = await _movieService.ObtenerDetalles(id);
        return movie == null ? NotFound() : View(movie);
    }

    // POST: MOVIES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        try
        {
            await _movieService.EliminarPelicula(id);
            return RedirectToAction(nameof(Index));
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    private bool MovieExists(int? id)
    {
        return _context.Movie.Any(e => e.Id == id);
    }
}
