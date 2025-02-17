using Application.DTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;


namespace CinemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var movies = await _movieService.GetAllMoviesAsync();
            return Ok(movies);
        }

        

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var movie = await _movieService.GetMovieAsync(id);

            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MovieDTO movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var movie = await _movieService.CreateMovieAsync(movieDto);

            return Ok(movie);
        }

        
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] MovieDTO movieDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            

            var movie = await _movieService.UpdateMovieAsync(movieDTO);

            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _movieService.DeleteMovieAsync(id);

            if(movie == null) 
            { 
                return NotFound(); 
            }

            return Ok(movie);
        }
    }
}
