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
        public class BookingRepository : IBookingRepository
        {
            private readonly Dbcontext context;

            public BookingRepository(Dbcontext _context)
            {
                context = _context;
            }

            public Booking Create(Booking booking)
            {
                context.Bookings.Add(booking);
                context.SaveChanges();
                return booking;
            }
            public Booking GetById(int Id)
            {
                var result = context.Bookings.FirstOrDefault(c => c.BookingID == Id);
                return result;
            }

            //TODO: Get All Seat
            public List<Booking> GetAll()
            {
                var result = context.Bookings.ToList();
                return result;
            }

            public void DeleteByID(int Id)
            {
                var booking = context.Bookings.FirstOrDefault(c => c.BookingID == Id);
                if (booking != null)
                {
                    context.Remove(booking);
                    context.SaveChanges();
                }
            }
        }
    }