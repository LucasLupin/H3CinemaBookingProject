using H3CinemaBooking.Repository.DTO;
using H3CinemaBooking.Repository.Interfaces;
using H3CinemaBooking.Repository.Models;
using H3CinemaBooking.Repository.Models.DTO_s;
using System.Collections.Generic;
using System.Linq;

namespace H3CinemaBooking.Repository.Service
{
    public class AdminUserService
    {
        private readonly IAdminUserRepository _adminUserRepository;
        private readonly HashingService _hashingService = new HashingService();

        public AdminUserService(IAdminUserRepository adminUserRepository)
        {
            _adminUserRepository = adminUserRepository;
        }

        public List<AdminUserDTO> GetAll()
        {
            var admin = _adminUserRepository.GetAll();
            return admin.Select(c => new AdminUserDTO
            {
                AdminUserID = c.AdminUserID,
                Name = c.Name,
                Email = c.Email,
            }).ToList();
        }

        public AdminUserDTO GetById(int id)
        {
            var admin = _adminUserRepository.GetById(id);
            if (admin == null) return null;
            return new AdminUserDTO
            {
                AdminUserID = admin.AdminUserID,
                Name = admin.Name,
                Email = admin.Email
            };
        }

        public (string Hash,string Salt) Create(AdminUser admin)
        {
            string newSalt = _hashingService.GenerateSalt();
            string newHash = _hashingService.HashPassword(admin.PasswordHash, newSalt);
            admin.PasswordSalt = newSalt;
            admin.PasswordHash = newHash;

            return (newHash, newSalt);
        }
        public bool Delete(int id)
        {
            var admin = _adminUserRepository.GetById(id);
            if (admin != null)
            {
                _adminUserRepository.DeleteByID(id);
                return true;
            }
            return false;
        }


    }
}
