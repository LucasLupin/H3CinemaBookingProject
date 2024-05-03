using H3CinemaBooking.Repository.Data;
using H3CinemaBooking.Repository.Interfaces;
using H3CinemaBooking.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3CinemaBooking.Repository.Repositories
{
    public class UserDetailRepository : IUserDetailRepository
    {
        private readonly Dbcontext context;

        public UserDetailRepository(Dbcontext _context)
        {
            context = _context;
        }

        public UserDetail Create(UserDetail userDetail)
        {
            context.UserDetails.Add(userDetail);
            context.SaveChanges();
            return userDetail;
        }
        public UserDetail GetById(int Id)
        {
            var result = context.UserDetails.FirstOrDefault(c => c.UserDetailID == Id);
            return result;
        }

        public UserDetail GetByEmail(string Email)
        {
            var result = context.UserDetails.FirstOrDefault(c => c.Email == Email);
            return result;
        }

        public UserDetail GetByPhoneNumber(string PhoneNumber)
        {
            var result = context.UserDetails.FirstOrDefault(c => c.PhoneNumber == PhoneNumber);
            return result;
        }

        //TODO: Get All UserDetails
        public List<UserDetail> GetAll()
        {
            var result = context.UserDetails.ToList();
            return result;
        }

        public void DeleteByID(int Id, UserDetail userDetail)
        {
            if (userDetail != null)
            {
                // Set IsActive to false instead of removing the customer
                userDetail.IsActive = false;
                context.Update(userDetail);
                context.SaveChanges();
            }
        }

        public Roles GetRole(int roleId)
        {
            var result = context.Roles.FirstOrDefault(r => r.RoleID == roleId);
            return result;
        }


        // Create a login

    }
}
