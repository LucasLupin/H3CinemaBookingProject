using Microsoft.AspNetCore.Mvc;
using H3CinemaBooking.Repository.Interfaces;
using H3CinemaBooking.Repository.Models;
using System.Collections.Generic;
using H3CinemaBooking.Repository.Repositories;

namespace H3CinemaBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemaController : ControllerBase
    {
        private readonly IGenericRepository<Cinema> _cinemaRepository;

        public CinemaController(IGenericRepository<Cinema> cinemaRepository)
        {
            _cinemaRepository = cinemaRepository;
        }

        // GET: api/<CinemaController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cinema>>> GetAll()
        {
            var cinemas = await _cinemaRepository.GetAllAsync();
            return Ok(cinemas);
        }

        // GET api/<CinemaController>/5
        [HttpGet("{id}")]
        public ActionResult<Cinema> GetById(int id)
        {
            var cinema = _cinemaRepository.GetById(id);
            if (cinema == null)
            {
                return NotFound();
            }
            return Ok(cinema);
        }

        // POST api/<CinemaController>
        [HttpPost]
        public ActionResult<Cinema> Post([FromBody] Cinema cinema)
        {
            if (cinema == null)
            {
                return BadRequest("Cinema data is required");
            }
            _cinemaRepository.Create(cinema);
            return Ok("Cinema created successfully.");
        }

        // DELETE api/<CinemaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var cinema = _cinemaRepository.GetById(id);
            if (cinema == null)
            {
                return NotFound();
            }
            _cinemaRepository.DeleteById(id);
            return Ok("Cinema deleted successfully.");
        }
    }
}
