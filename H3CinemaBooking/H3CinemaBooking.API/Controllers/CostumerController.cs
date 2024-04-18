using Microsoft.AspNetCore.Mvc;
using H3CinemaBooking.Repository.Service;
using H3CinemaBooking.Repository.Models;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace H3CinemaBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostumerController : ControllerBase
    {
        private readonly CostumerService _costumerService;

        public CostumerController(CostumerService costumerService)
        {
            _costumerService = costumerService;
        }

        [HttpGet]
        public ActionResult<List<Costumer>> GetAll()
        {
            var result = _costumerService.GetAllCostumers();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<Costumer> GetByID(int id)
        {
            var costumer = _costumerService.GetCostumerById(id);
            if (costumer == null)
            {
                return NotFound();
            }
            return Ok(costumer);
        }

        [HttpPost]
        public ActionResult<Costumer> Post(Costumer costumer)
        {
            var (hash, salt) = _costumerService.CreateCustomer(costumer);
            if (string.IsNullOrEmpty(hash) || string.IsNullOrEmpty(salt))
            {
                return BadRequest("Failed to create customer due to password hashing failure.");
            }
            return Ok(costumer);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _costumerService.DeleteCostumer(id);
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
