using H3CinemaBooking.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3CinemaBooking.Repository.Interfaces
{
    public interface IShowRepository
    {
        Show Create(Show show);
        Show GetById(int id);
        List<Show> GetAll();
        void DeleteByID(int id, Show show);
    }
}
