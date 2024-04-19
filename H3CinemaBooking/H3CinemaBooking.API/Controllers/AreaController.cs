using Microsoft.AspNetCore.Mvc;
using H3CinemaBooking.Repository.Interfaces;
using H3CinemaBooking.Repository.Models;
using System.Collections.Generic;
using H3CinemaBooking.Repository.Repositories;

namespace H3CinemaBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly IGenericRepository<Area> _areaRepository;

        public AreaController(IGenericRepository<Area> areaRepository)
        {
            _areaRepository = areaRepository;
        }

        // GET: api/<AreaController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Area>>> GetAll()
        {
            var areas = await _areaRepository.GetAllAsync();
            return Ok(areas);
        }

        // GET api/<AreaController>/5
        [HttpGet("{id}")]
        public ActionResult<Area> GetById(int id)
        {
            var area = _areaRepository.GetById(id);
            if (area == null)
            {
                return NotFound();
            }
            return Ok(area);
        }

        // POST api/<AreaController>
        [HttpPost]
        public ActionResult<Area> Post([FromBody] Area area)
        {
            if (area == null)
            {
                return BadRequest("Area data is required");
            }
            _areaRepository.Create(area);
            return Ok("Cinema created successfully.");
        }

        // DELETE api/<AreaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var area = _areaRepository.GetById(id);
            if (area == null)
            {
                return NotFound();
            }
            _areaRepository.DeleteById(id);
            return Ok("Cinema deleted successfully.");
        }
    }
}
