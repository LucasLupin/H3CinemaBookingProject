using Microsoft.AspNetCore.Mvc;
using H3CinemaBooking.Repository.Interfaces;
using H3CinemaBooking.Repository.Models;
using System.Security.Cryptography;
using H3CinemaBooking.Repository.Models.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace H3CinemaBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IBookingService _bookingService;
        public BookingController(IBookingRepository bookingRepository, IBookingService bookingService)  
        { 
            _bookingRepository = bookingRepository;
            _bookingService = bookingService;
        }

        //TODO: Make a Get all here
        // GET: api/<BookingController>
        [HttpGet]
        public ActionResult<List<Booking>> GetAll()
        {
            var result = _bookingRepository.GetAll();
            return Ok(result);
        }

        // GET api/<BookingController>/id
        [HttpGet("{id}")]
        public ActionResult<Booking>GetByID(int id)
        {
            var booking = _bookingRepository.GetById(id);
            if(booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        // POST api/<BookingController>
        [HttpPost("reserve")]
        public ActionResult<ReserveSeatDTO> Post(ReserveSeatDTO reserveSeat)
        {
            ////_bookingRepository.Create(booking);
            _bookingService.ReserveSeats(reserveSeat);
            return Ok("Customer created successfully.");
        }

        // POST api/<BookingController>
        [HttpPost]
        public ActionResult<Booking>Post(Booking booking)
        {
            _bookingRepository.Create(booking);
            return Ok("Customer created successfully.");
        }

        // DELETE api/<BookingController>/ID
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _bookingRepository.DeleteByID(id);
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
