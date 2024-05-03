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
    public class ShowRepository : IShowRepository
    {
        private readonly Dbcontext context;

        public ShowRepository(Dbcontext _context)
        {
            context = _context;
        }

        public Show Create(Show show)
        {
            context.Shows.Add(show);
            context.SaveChanges();
            return show;
        }
        public Show GetById(int Id)
        {
            var result = context.Shows.FirstOrDefault(c => c.ShowID == Id);
            return result;
        }

        //TODO: Get All Costumer
        public List<Show> GetAll()
        {
            var result = context.Shows.ToList();
            return result;
        }

        public void UpdateByID(int Id, Show updatedShow)
        {
            var show = context.Shows.FirstOrDefault(s => s.ShowID == Id);

            if (show != null)
            {
                show.HallID = updatedShow.HallID;
                show.MovieID = updatedShow.MovieID;
                show.Price = updatedShow.Price;
                show.ShowDateTime = updatedShow.ShowDateTime;

                context.SaveChanges();
            }
        }

        public void DeleteByID(int Id, Show show)
        {
            if (show != null)
            {
                context.Remove(show);
                context.SaveChanges();
            }
        }
    }
}
