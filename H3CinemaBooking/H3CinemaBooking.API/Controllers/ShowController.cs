using Microsoft.AspNetCore.Mvc;
using H3CinemaBooking.Repository.Interfaces;
using H3CinemaBooking.Repository.Models;
using System.Security.Cryptography;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace H3CinemaBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowController : ControllerBase
    {
        private readonly IShowRepository _showRepository;
        public ShowController(IShowRepository showRepository) => _showRepository = showRepository;

        //TODO: Make a Get all here
        // GET: api/<ShowController>
        [HttpGet]
        public ActionResult<List<Show>> GetAll()
        {
            var result = _showRepository.GetAll();
            return Ok(result);
        }

        // GET api/<ShowController>/id
        [HttpGet("{id}")]
        public ActionResult<Show>GetByID(int id)
        {
            var show = _showRepository.GetById(id);
            if(show == null)
            {
                return NotFound();
            }
            return Ok($"Hello from ShowController Get {show}");
        }

        // POST api/<Showtroller>
        [HttpPost]
        public ActionResult<Show>Post(Show show)
        {   
            _showRepository.Create(show);
            return Ok("Show created successfully.");
        }

        // DELETE api/<CinemaHallController>/ID
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var show = _showRepository.GetById(id);
            if (show == null)
            {
                return NotFound();
            }

            _showRepository.DeleteByID(id, show);
            return Ok();
        }


        //TODO: Make a update here
        // PUT api/<CinemaHallController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
    }
}
