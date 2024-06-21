using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPI.Models;

namespace RestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController: ControllerBase
{
    private readonly MovieContext _movieContext;

    public MoviesController(MovieContext movieContext)
    {
        _movieContext = movieContext;
    }

    // Get : api/movies
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
    {
        if (_movieContext.Movies == null)
        {
            return NotFound();
        }

        return await _movieContext.Movies.ToListAsync();
    }

    // Get : api/movies/2
    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> GetMovie(int id)
    {
        if (_movieContext.Movies == null)
        {
            return NotFound();
        }

        var movie = await _movieContext.Movies.FindAsync(id);
        if (movie is null)
        {
            return NotFound();
        }

        return movie;
    }

    // Post : api/movies
    [HttpPost]
    public async Task<ActionResult<Movie>> PostMovie(Movie movie)
    {
        if (_movieContext.Movies == null)
        {
            return NotFound();
        }

        _movieContext.Movies.Add(movie);
        await _movieContext.SaveChangesAsync();
        return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
    }

    // Put : api/Movies/2
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMovie(int id, Movie movie)
    {
        if (id != movie.Id)
        {
            return BadRequest();
        }

        _movieContext.Entry(movie).State = EntityState.Modified;
        try
        {
            await _movieContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MovieExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    private bool MovieExists(int id)
    {
        return (_movieContext.Movies?.Any(e => e.Id == id)).GetValueOrDefault();
    }

    // Delete : api/movies/2
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        if (_movieContext.Movies == null)
        {
            return NotFound();
        }

        var movie = await _movieContext.Movies.FindAsync(id);
        if (movie is null)
        {
            return NotFound();
        }

        _movieContext.Movies.Remove(movie);
        await _movieContext.SaveChangesAsync();
        return NoContent();
    }
}