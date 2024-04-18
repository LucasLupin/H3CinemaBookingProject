using H3CinemaBooking.Repository.Interfaces;
using H3CinemaBooking.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace H3CinemaBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository _movieGenreRepository;

        public GenreController(IGenreRepository movieGenreRepository)
        {
            _movieGenreRepository = movieGenreRepository;
        }

        //TODO: Make a Get all here
        // GET: api/<MovieGenreController>
        [HttpGet]
        public ActionResult<List<Genre>> GetAll()
        {
            var result = _movieGenreRepository.GetAll();
            return Ok($"Hello From MovieGenreController GetAll Result: {result}");
        }

        // GET api/<MovieGenreController>/id
        [HttpGet("{id}")]
        public ActionResult<Genre> GetByID(int id)
        {
            var movie = _movieGenreRepository.GetById(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok($"Hello from MovieGenreController Get {movie}");
        }

        // POST api/<MovieGenreController>
        [HttpPost]
        public ActionResult<Genre> Post(Genre genre)
        {
            _movieGenreRepository.Create(genre);
            return Ok("MovieGenre created successfully.");
        }

        // DELETE api/<MovieGenreController>/ID
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _movieGenreRepository.DeleteGenreByID(id);
            return Ok();
        }
    }
}
