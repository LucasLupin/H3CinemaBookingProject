using H3CinemaBooking.Repository.Interfaces;
using H3CinemaBooking.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace H3CinemaBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        //TODO: Make a Get all here
        // GET: api/<MovieController>
        [HttpGet]
        public ActionResult<List<Movie>> GetAll()
        {
            var result = _movieRepository.GetAll();
            return Ok($"Hello From MovieController GetAll Result: {result}");
        }

        // GET api/<MovieController>/id
        [HttpGet("{id}")]
        public ActionResult<Movie> GetByID(int id)
        {
            var movie = _movieRepository.GetById(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok($"Hello from MovieController Get {movie}");
        }

        // POST api/<MovieController>
        [HttpPost("Simple")]
        public ActionResult<Movie> Post(Movie movie)
        {
            _movieRepository.Create(movie);
            return Ok("Movie created successfully.");
        }

        [HttpPost("Complex")]
        public ActionResult<Movie> PostComplex(Movie movie)
        {
            try
            {
                var genreNames = movie.Genres
                                      .SelectMany(g => g.GenreName.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                                      .Select(gn => gn.Trim())
                                      .ToList();
                movie.Genres = null; // Clear genres to avoid processing existing references

                var resultMovie = _movieRepository.CreateComplex(movie, genreNames);
                return Ok(resultMovie); 
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }



        // DELETE api/<MovieController>/ID
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _movieRepository.DeleteByID(id);
            return Ok();
        }
    }
}
