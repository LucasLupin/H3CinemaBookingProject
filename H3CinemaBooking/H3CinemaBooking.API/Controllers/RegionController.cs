using Microsoft.AspNetCore.Mvc;
using H3CinemaBooking.Repository.Interfaces;
using H3CinemaBooking.Repository.Models;
using System.Collections.Generic;
using H3CinemaBooking.Repository.Repositories;

namespace H3CinemaBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IGenericRepository<Region> _regionRepository;

        public RegionController(IGenericRepository<Region> regionRepository)
        {
            _regionRepository = regionRepository;
        }

        // GET: api/<RegionController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Region>>> GetAll()
        {
            var regions = await _regionRepository.GetAllAsync();
            return Ok(regions);
        }

        // GET api/<RegionController>/5
        [HttpGet("{id}")]
        public ActionResult<Region> GetById(int id)
        {
            var region = _regionRepository.GetById(id);
            if (region == null)
            {
                return NotFound();
            }
            return Ok(region);
        }

        // POST api/<RegionController>
        [HttpPost]
        public ActionResult<Region> Post([FromBody] Region region)
        {
            if (region == null)
            {
                return BadRequest("Cinema data is required");
            }
            _regionRepository.Create(region);
            return Ok(region);
        }

        //Update Api Movie with Genre
        [HttpPut("{id}")]
        public ActionResult Update(int id, Region region)
        {
            var existingRegion = _regionRepository.GetById(id);

            if (existingRegion != null)
            {
                existingRegion.RegionName = region.RegionName;
            }
            _regionRepository.Update(existingRegion);
            return Ok(existingRegion);
        }

        // DELETE api/<CinemaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var region = _regionRepository.GetById(id);
            if (region == null)
            {
                return NotFound();
            }
            _regionRepository.DeleteById(id);
            return Ok();
        }
    }
}
