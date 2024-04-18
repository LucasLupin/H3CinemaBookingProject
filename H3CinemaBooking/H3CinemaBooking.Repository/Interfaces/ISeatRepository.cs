using H3CinemaBooking.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3CinemaBooking.Repository.Interfaces
{
    public interface ISeatRepository
    {
        Seat Create(Seat seat);
        Seat GetById(int id);
        List<Seat> GetAll();
        void DeleteByID(int id);
    }
}
