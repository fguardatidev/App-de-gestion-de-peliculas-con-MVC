
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;
using MvcMovie.Services;

public class MoviesController : Controller
{
    private readonly MvcMovieContext _context;
    private readonly IMovieService _movieService;

    public MoviesController(MvcMovieContext context, MovieService movieService)
    {
        _context = context;
        _movieService = movieService;
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
    public IActionResult Create()
    {
        return View();
    }

    // POST: MOVIES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price,Rating,Director,Duration,Seen,PersonalRating")] Movie movie)
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
        var movie = await _movieService.ObtenerDetalles(id);
        return movie == null ? NotFound() : View(movie);
    }

    // POST: MOVIES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Title,ReleaseDate,Genre,Price,Rating,Director,Duration,Seen,PersonalRating")] Movie movie)
    {
        if (id != movie.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(movie);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(movie.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(movie);
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
