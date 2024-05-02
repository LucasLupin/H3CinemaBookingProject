using Microsoft.AspNetCore.Mvc;
using H3CinemaBooking.Repository.Interfaces;
using H3CinemaBooking.Repository.Models;
using System.Collections.Generic;
using H3CinemaBooking.Repository.Repositories;

namespace H3CinemaBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IGenericRepository<Roles> _roleRepository;

        public RoleController(IGenericRepository<Roles> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        // GET: api/<CinemaController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Roles>>> GetAll()
        {
            var roles = await _roleRepository.GetAllAsync();
            return Ok(roles);
        }

        // GET api/<CinemaController>/5
        [HttpGet("{id}")]
        public ActionResult<Roles> GetById(int id)
        {
            var role = _roleRepository.GetById(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        // POST api/<CinemaController>
        [HttpPost]
        public ActionResult<Roles> Post([FromBody] Roles role)
        {
            if (role == null)
            {
                return BadRequest("Cinema data is required");
            }
            _roleRepository.Create(role);
            return Ok(role);
        }

        //Update Api Movie with Genre
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Roles role)
        {
            var existingRole = _roleRepository.GetById(id);

            if (existingRole != null)
            {
                existingRole.RoleName = role.RoleName;
            }
            _roleRepository.Update(existingRole);
            return Ok();
        }


        // DELETE api/<CinemaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var role = _roleRepository.GetById(id);
            if (role == null)
            {
                return NotFound();
            }
            _roleRepository.DeleteById(id);
            return Ok();
        }
    }
}
