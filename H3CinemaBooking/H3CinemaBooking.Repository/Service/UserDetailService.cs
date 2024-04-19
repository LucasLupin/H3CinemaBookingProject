using H3CinemaBooking.Repository.DTO;
using H3CinemaBooking.Repository.Interfaces;
using H3CinemaBooking.Repository.Models;
using H3CinemaBooking.Repository.Models.DTO_s;
using System.Collections.Generic;
using System.Linq;

namespace H3CinemaBooking.Repository.Service
{
    public class UserDetailService : IUserDetailService
    {
        private readonly IUserDetailRepository _userDetailRepository;
        private readonly HashingService _hashingService = new HashingService();

        public UserDetailService(IUserDetailRepository userDetailRepository)
        {
            _userDetailRepository = userDetailRepository;
        }

        public List<UserDetailDTO> GetAllUserDetail()
        {
            var userdetail = _userDetailRepository.GetAll();
            return userdetail.Select(c => new UserDetailDTO
            {
                UserDetailID = c.UserDetailID,
                Name = c.Name,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                RoleID = c.RoleID,
                Bookings = c.Bookings,
                IsActive = c.IsActive
            }).ToList();
        }

        public UserDetailDTO GetUserDetailById(int id)
        {
            var userdetail = _userDetailRepository.GetById(id);
            if (userdetail == null) return null;
            return new UserDetailDTO
            {
                UserDetailID = userdetail.UserDetailID,
                Name = userdetail.Name,
                Email = userdetail.Email,
                PhoneNumber = userdetail.PhoneNumber,
                RoleID= userdetail.RoleID,
                Bookings = userdetail.Bookings,
                IsActive = userdetail.IsActive
            };
        }

        public (string Hash,string Salt) CreateUserDetail(UserDetail userdetail)
        {
            string newSalt = _hashingService.GenerateSalt();
            string newHash = _hashingService.HashPassword(userdetail.PasswordHash, newSalt);
            userdetail.PasswordSalt = newSalt;
            userdetail.PasswordHash = newHash;

            return (newHash, newSalt);
        }
        public bool DeleteUserdetail(int id)
        {
            var userdetail = _userDetailRepository.GetById(id);
            if (userdetail != null)
            {
                _userDetailRepository.DeleteByID(id, userdetail);
                return true;
            }
            return false;
        }


    }
}
