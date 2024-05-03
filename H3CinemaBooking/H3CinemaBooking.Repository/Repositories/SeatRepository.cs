using H3CinemaBooking.Repository.Data;
using H3CinemaBooking.Repository.Interfaces;
using H3CinemaBooking.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3CinemaBooking.Repository.Repositories
{
        public class SeatRepository : ISeatRepository
        {
            private readonly Dbcontext context;

            public SeatRepository(Dbcontext _context)
            {
                context = _context;
            }

            public Seat Create(Seat seat)
            {
                context.Seats.Add(seat);
                context.SaveChanges();
                return seat;
            }

            public IEnumerable<Seat> CreateBulk(IEnumerable<Seat> seats)
            {
                context.Seats.AddRange(seats);
                context.SaveChanges();
                return seats;
            }

        public Seat GetById(int Id)
            {
                var result = context.Seats.FirstOrDefault(c => c.SeatID == Id);
                return result;
            }

            //TODO: Get All Seat
            public List<Seat> GetAll()
            {
                var result = context.Seats.ToList();
                return result;
            }

            public void DeleteByID(int Id)
            {
                var seat = context.Seats.FirstOrDefault(c => c.SeatID == Id);
                if (seat != null)
                {
                    context.Remove(seat);
                    context.SaveChanges();
                }
            }
        }
    }