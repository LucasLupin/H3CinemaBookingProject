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
    public class CostumerRepository : ICostumerRepository
    {
        private readonly Dbcontext context;

        public CostumerRepository(Dbcontext _context)
        {
            context = _context;
        }

        public Costumer Create(Costumer costumer)
        {
            context.Costumers.Add(costumer);
            context.SaveChanges();
            return costumer;
        }
        public Costumer GetById(int Id)
        {
            var result = context.Costumers.FirstOrDefault(c => c.CostumerID == Id);
            return result;
        }

        //TODO: Get All Costumer
        public List<Costumer> GetAll()
        {
            var result = context.Costumers.ToList();
            return result;
        }

        public void DeleteByID(int Id, Costumer costumer)
        {
            if (costumer != null)
            {
                // Set IsActive to false instead of removing the customer
                costumer.IsActive = false;
                context.Update(costumer);
                context.SaveChanges();
            }
        }
    }
}
