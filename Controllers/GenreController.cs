
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;
using MvcMovie.Services;

public class GenreController : Controller
{
    private readonly MvcMovieContext _context;
    private readonly IGenreService _genreService;

    public GenreController(MvcMovieContext context, IGenreService genreService)
    {
        _context = context;
        _genreService = genreService;
    }

    // GET: GENRES
    public async Task<IActionResult> Index()    
    {
        return View(await _genreService.ObtenerGeneros());
    }

    // GET: GENRES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        var genre = await _genreService.ObtenerDetalles(id);

        if (genre == null)
        {
            return NotFound();
        }

        return View(genre);
    }

    // GET: GENRES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: GENRES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name")] Genre genre)
    {
        if(ModelState.IsValid)
        {
            await _genreService.CrearGenero(genre);
            return RedirectToAction(nameof(Index));
        }

        return View(genre);
    }

    // GET: GENRES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        var genre = await _genreService.ObtenerDetalles(id);

        if (genre == null)
        {
            return NotFound();
        }

        return View(genre);
    }

    // POST: GENRES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Name")] Genre genre)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var result = await _genreService.EditarGenero(id, genre);
                if(result == null)
                {
                    return NotFound();
                }
            }
            catch
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(genre);
    }

    // GET: GENRES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        var genre = await _genreService.ObtenerDetalles(id);

        if (genre == null)
        {
            return NotFound();
        }

        return View(genre);
    }

    // POST: GENRES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        await _genreService.EliminarGenero(id);
        return RedirectToAction(nameof(Index));
    }
}
