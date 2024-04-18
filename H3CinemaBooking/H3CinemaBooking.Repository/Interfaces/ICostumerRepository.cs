using H3CinemaBooking.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3CinemaBooking.Repository.Interfaces
{
    public interface ICostumerRepository
    {
        Costumer Create(Costumer costumer);
        Costumer GetById(int id);
        List<Costumer> GetAll();
        void DeleteByID(int id, Costumer costumer);
    }
}
