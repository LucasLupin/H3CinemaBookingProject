using Microsoft.AspNetCore.Mvc;
using H3CinemaBooking.Repository.Service;
using H3CinemaBooking.Repository.Models;
using System.Security.Cryptography;
using System.Collections.Generic;
using H3CinemaBooking.Repository.Interfaces;

namespace H3CinemaBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailController : ControllerBase
    {
        private readonly IUserDetailService _userDetailService;

        public UserDetailController(IUserDetailService userDetailService)
        {
            _userDetailService = userDetailService;
        }


        [HttpGet]
        public ActionResult<List<UserDetail>> GetAll()
        {
            var result = _userDetailService.GetAllUserDetail();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<UserDetail> GetByID(int id)
        {
            var userdetail = _userDetailService.GetUserDetailById(id);
            if (userdetail == null)
            {
                return NotFound();
            }
            return Ok(userdetail);
        }

        [HttpPost]
        public ActionResult<UserDetail> Post(UserDetail userDetail)
        {
            var (hash, salt) = _userDetailService.CreateUserDetail(userDetail);
            if (string.IsNullOrEmpty(hash) || string.IsNullOrEmpty(salt))
            {
                return BadRequest("Failed to create customer due to password hashing failure.");
            }
            return Ok(userDetail);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _userDetailService.DeleteUserdetail(id);
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
