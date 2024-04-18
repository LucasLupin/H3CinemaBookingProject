using H3CinemaBooking.Repository.Data;
using H3CinemaBooking.Repository.Interfaces;
using H3CinemaBooking.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3CinemaBooking.Repository.Repositories
{
        public class AdminUserRepository : IAdminUserRepository
        {
            private readonly Dbcontext context;

            public AdminUserRepository(Dbcontext _context)
            {
                context = _context;
            }

            public AdminUser Create(AdminUser adminuser)
            {
                context.AdminUsers.Add(adminuser);
                context.SaveChanges();
                return adminuser;
            }
            public AdminUser GetById(int Id)
            {
                var result = context.AdminUsers.FirstOrDefault(c => c.AdminUserID == Id);
                return result;
            }

            //TODO: Get All Costumer
            public List<AdminUser> GetAll()
            {
                var result = context.AdminUsers.ToList();
                return result;
            }

            public void DeleteByID(int Id)
            {
                var adminuser = context.AdminUsers.FirstOrDefault(c => c.AdminUserID == Id);
                if (adminuser != null)
                {
                    context.Remove(adminuser);
                    context.SaveChanges();
                }
            }
        }
    }