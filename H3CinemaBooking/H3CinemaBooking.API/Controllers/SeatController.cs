using Microsoft.AspNetCore.Mvc;
using H3CinemaBooking.Repository.Interfaces;
using H3CinemaBooking.Repository.Models;
using System.Security.Cryptography;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace H3CinemaBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private readonly ISeatRepository _seatRepository;
        public SeatController(ISeatRepository seatRepository) => _seatRepository = seatRepository;

        //TODO: Make a Get all here
        // GET: api/<SeatController>
        [HttpGet]
        public ActionResult<List<Seat>> GetAll()
        {
            var result = _seatRepository.GetAll();
            return Ok(result);
        }

        // GET api/<SeatController>/id
        [HttpGet("{id}")]
        public ActionResult<Seat>GetByID(int id)
        {
            var seat = _seatRepository.GetById(id);
            if(seat == null)
            {
                return NotFound();
            }
            return Ok(seat);
        }

        // POST api/<SeatController>
        [HttpPost]
        public ActionResult<Seat>Post(Seat seat)
        {
            _seatRepository.Create(seat);
            return Ok("Seat created successfully.");
        }

        // DELETE api/<SeatController>/ID
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _seatRepository.DeleteByID(id);
            return Ok();
        }


        //TODO: Make a update here
        // PUT api/<CostumerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
    }
}
