using Microsoft.AspNetCore.Mvc;
using H3CinemaBooking.Repository.Service;
using H3CinemaBooking.Repository.Models;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace H3CinemaBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUserController : ControllerBase
    {
        private readonly AdminUserService _adminservice;

        public AdminUserController(AdminUserService adminUserService)
        {
            _adminservice = adminUserService;
        }

        [HttpGet]
        public ActionResult<List<AdminUser>> GetAll()
        {
            var result = _adminservice.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<AdminUser> GetByID(int id)
        {
            var admin = _adminservice.GetById(id);
            if (admin == null)
            {
                return NotFound();
            }
            return Ok(admin);
        }

        [HttpPost]
        public ActionResult<AdminUser> Post(AdminUser admin)
        {
            var (hash, salt) = _adminservice.Create(admin);
            if (string.IsNullOrEmpty(hash) || string.IsNullOrEmpty(salt))
            {
                return BadRequest("Failed to create customer due to password hashing failure.");
            }
            return Ok(admin);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _adminservice.Delete(id);
            return Ok();
        }

        //[HttpPut("{id}")]
        //public ActionResult Put(int id, [FromBody] Costumer costumer)
        //{
        //    var existingCostumer = _costumerService.GetCostumerById(id);
        //    if (existingCostumer == null)
        //    {
        //        return NotFound();
        //    }

        //    _costumerService.UpdateCostumer(id, costumer);
        //    return NoContent();
        //}
    }
}
