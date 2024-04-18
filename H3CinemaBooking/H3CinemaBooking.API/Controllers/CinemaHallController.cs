using Microsoft.AspNetCore.Mvc;
using H3CinemaBooking.Repository.Interfaces;
using H3CinemaBooking.Repository.Models;
using System.Security.Cryptography;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace H3CinemaBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemaHallController : ControllerBase
    {
        private readonly IGenericRepository<CinemaHall> _cinemaHallRepository;

        public CinemaHallController(IGenericRepository<CinemaHall> cinemaHallRepository)
        {
            _cinemaHallRepository = cinemaHallRepository;
        }

        // GET: api/<CinemaHallController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CinemaHall>>> GetAll()
        {
            var cinemaHalls = await _cinemaHallRepository.GetAllAsync();
            return Ok(cinemaHalls);
        }

        // GET api/<CinemaHallController>/id
        [HttpGet("{id}")]
        public ActionResult<CinemaHall>GetByID(int id)
        {
            var cinemaHall = _cinemaHallRepository.GetById(id);
            if(cinemaHall == null)
            {
                return NotFound();
            }
            return Ok(cinemaHall);
        }

        // POST api/<CinemaHallController>
        [HttpPost]
        public ActionResult<CinemaHall>Post(CinemaHall cinemaHall)
        {   
            _cinemaHallRepository.Create(cinemaHall);
            return Ok("CinemaHall created successfully.");
        }

        // DELETE api/<CinemaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var cinemaHall = _cinemaHallRepository.GetById(id);
            if (cinemaHall == null)
            {
                return NotFound();
            }
            _cinemaHallRepository.DeleteById(id);
            return Ok("Cinema deleted successfully.");
        }


        //TODO: Make a update here
        // PUT api/<CinemaHallController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
    }
}
