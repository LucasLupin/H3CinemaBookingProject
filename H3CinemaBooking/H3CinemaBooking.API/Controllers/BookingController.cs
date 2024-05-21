using Microsoft.AspNetCore.Mvc;
using H3CinemaBooking.Repository.Interfaces;
using H3CinemaBooking.Repository.Models;
using System.Security.Cryptography;
using H3CinemaBooking.Repository.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace H3CinemaBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IBookingService _bookingService;
        private readonly IJWTokenService _jwtTokenService;
        public BookingController(IBookingRepository bookingRepository, IBookingService bookingService, IJWTokenService jWTokenService)  
        { 
            _bookingRepository = bookingRepository;
            _bookingService = bookingService;
            _jwtTokenService = jWTokenService;
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
        [Authorize(Roles = "Admin,Customer")]
        public ActionResult<ReserveSeatResultDTO> Post(ReserveSeatDTO reserveSeat)
            {

            if (reserveSeat.SeatList == null || reserveSeat.ShowID == 0)
            {
                return BadRequest("Invalid data received.");
            }

            var userId = _jwtTokenService.GetUserIDFromToken(User);
            if (userId == null)
            {
                return Unauthorized("UserID not found in token");
            }

            reserveSeat.UserID = userId.Value;

            var result = _bookingService.ReserveSeats(reserveSeat);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("latest")]
        [Authorize(Roles = "Admin,Customer")]
        public ActionResult<BookingDTO> GetLatestBooking()
        {
            //var userId = 1;
            var userId = _jwtTokenService.GetUserIDFromToken(User);
            if (userId == null)
            {
                return Unauthorized("UserID not found in token");
            }



            //var latestBooking = _bookingService.GetLatestBookingForUser(userId.Value);
            var latestBooking = _bookingService.GetLatestBookingForUser(userId.Value);
            if (latestBooking == null)
            {
                return NotFound("No booking found for the user");
            }

            return Ok(latestBooking);
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
