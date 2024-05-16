using Microsoft.AspNetCore.Mvc;
using H3CinemaBooking.Repository.Interfaces;
using H3CinemaBooking.Repository.Models;
using System.Security.Cryptography;
using H3CinemaBooking.Repository.Models.DTO;
using System.Collections.Generic;


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

        // GET: api/<BookingController>
        [HttpGet]
        public ActionResult<List<Booking>> GetAll()
        {
            var result = _bookingRepository.GetAll();
            if (result == null || result.Count == 0)
            {
                return NoContent();
            }
            return Ok(result);
        }

        // GET api/<BookingController>/id
        [HttpGet("{id}")]
        public ActionResult<Booking> GetByID(int id)
        {
            var booking = _bookingRepository.GetById(id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        // POST api/<BookingController>/reserve
        [HttpPost("reserve")]
        public ActionResult<ReserveSeatDTO> Post(ReserveSeatDTO reserveSeat)
        {
            if (reserveSeat == null)
            {
                return BadRequest("Reservation data is required");
            }

            try
            {
                _bookingService.ReserveSeats(reserveSeat);
                return Ok("Reservation created successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<BookingController>
        [HttpPost]
        public ActionResult<Booking>Post(Booking booking)
        {
            if (booking == null)
            {
                return BadRequest("Booking data is required");
            }

            _bookingRepository.Create(booking);
            return Ok("Booking created successfully.");
        }

        // DELETE api/<BookingController>/ID
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (_bookingRepository.DeleteByID(id))
            {
                return Ok(new { message = "Booking deleted successfully" });
            }
            return NotFound("ID was not found!");
        }
    }
}
