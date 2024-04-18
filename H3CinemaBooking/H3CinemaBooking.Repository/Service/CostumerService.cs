using H3CinemaBooking.Repository.DTO;
using H3CinemaBooking.Repository.Interfaces;
using H3CinemaBooking.Repository.Models;
using H3CinemaBooking.Repository.Models.DTO_s;
using System.Collections.Generic;
using System.Linq;

namespace H3CinemaBooking.Repository.Service
{
    public class CostumerService
    {
        private readonly ICostumerRepository _costumerRepository;
        private readonly HashingService _hashingService = new HashingService();

        public CostumerService(ICostumerRepository costumerRepository)
        {
            _costumerRepository = costumerRepository;
        }

        public List<CostumerDTO> GetAllCostumers()
        {
            var costumers = _costumerRepository.GetAll();
            return costumers.Select(c => new CostumerDTO
            {
                CostumerID = c.CostumerID,
                Name = c.Name,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                IsActive = c.IsActive
            }).ToList();
        }

        public CostumerDTO GetCostumerById(int id)
        {
            var costumer = _costumerRepository.GetById(id);
            if (costumer == null) return null;
            return new CostumerDTO
            {
                CostumerID = costumer.CostumerID,
                Name = costumer.Name,
                Email = costumer.Email,
                PhoneNumber = costumer.PhoneNumber,
                IsActive = costumer.IsActive
            };
        }

        public (string Hash,string Salt) CreateCustomer(Costumer customer)
        {
            string newSalt = _hashingService.GenerateSalt();
            string newHash = _hashingService.HashPassword(customer.PasswordHash, newSalt);
            customer.PasswordSalt = newSalt;
            customer.PasswordHash = newHash;

            return (newHash, newSalt);
        }
        public bool DeleteCostumer(int id)
        {
            var costumer = _costumerRepository.GetById(id);
            if (costumer != null)
            {
                _costumerRepository.DeleteByID(id, costumer);
                return true;
            }
            return false;
        }


    }
}
