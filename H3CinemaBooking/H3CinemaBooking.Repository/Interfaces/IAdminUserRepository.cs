using H3CinemaBooking.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3CinemaBooking.Repository.Interfaces
{
    public interface IAdminUserRepository
    {
        AdminUser Create(AdminUser adminuser);
        AdminUser GetById(int id);
        List<AdminUser> GetAll();
        void DeleteByID(int id);
    }
}
