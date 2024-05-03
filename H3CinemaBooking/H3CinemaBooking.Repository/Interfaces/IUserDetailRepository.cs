using H3CinemaBooking.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3CinemaBooking.Repository.Interfaces
{
    public interface IUserDetailRepository
    {
        UserDetail Create(UserDetail userDetail);
        UserDetail GetById(int id);
        List<UserDetail> GetAll();
        void DeleteByID(int id, UserDetail userDetail);

        UserDetail GetByEmail(string email);
        UserDetail GetByPhoneNumber(string phoneNumber);

        Roles GetRole(int roleId);
    }
}
