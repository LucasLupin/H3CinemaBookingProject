using Microsoft.AspNetCore.Mvc;
using H3CinemaBooking.Repository.Interfaces;
using H3CinemaBooking.Repository.Models;
using System.Security.Cryptography;
using H3CinemaBooking.Repository.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace H3CinemaBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowController : ControllerBase
    {
        private readonly IShowRepository _showRepository;
        private readonly IShowService _showService;
        public ShowController(IShowRepository showRepository, IShowService showService) { 
            _showRepository = showRepository; 
            _showService = showService;
        }

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
        public ActionResult<Show> GetByID(int id)
        {
            var show = _showRepository.GetById(id);
            if (show == null)
            {
                return NotFound();
            }
            return Ok(show);
        }

        [HttpGet("bookInfo/{id}")]
        public ActionResult<BookShow> Get(int id)
        {
            var bookShow = _showService.SetBookShowObjekt(id);
            if (bookShow == null)
                return NotFound();
            return Ok(bookShow);
        }
 

        // POST api/<Showtroller>
        [HttpPost]
        public ActionResult<Show>Post(Show show)
        {   
            _showRepository.Create(show);
            return Ok(show);
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

        //Update Api Movie with Genre
        [HttpPut("{id}")]
        public ActionResult Update(int id, Show show)
        {
            _showRepository.UpdateByID(id, show);
            return Ok(show);
        }
    }
}
