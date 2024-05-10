using H3CinemaBooking.Repository.Data;
using H3CinemaBooking.Repository.Interfaces;
using H3CinemaBooking.Repository.Models;
using H3CinemaBooking.Repository.Models.DTO;
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

            public List<BookingSeat> GetBookingSeatsByShowId(int showId)
            {
                // First, get all BookingIDs for the specified showId
                var bookingIds = context.Bookings
                                        .Where(b => b.ShowID == showId)
                                        .Select(b => b.BookingID)
                                        .ToList(); // This will execute the query and return a List<int>

                // Then, filter BookingSeats by the obtained BookingIDs
                var result = context.BookingSeats
                                    .Where(bs => bookingIds.Contains(bs.BookingID))
                                    .ToList();

                return result;
            }

            public List<BookingSeat> CreateBookingSeat(Booking booking)
            {
                List<BookingSeat> bookingSeatList = new List<BookingSeat>();
                if (booking.BookingSeats != null)
                {
                    foreach (var bookingSeat in booking.BookingSeats)
                    {
                        //Check if the seatId already exists as bookingId
                        //If seatID already exists, continue
                        context.BookingSeats.Add(bookingSeat);
                        bookingSeatList.Add(bookingSeat);
                    }
                    context.SaveChanges();  // Gem alle ændringer på én gang
                    return bookingSeatList;
                }
                return null;
            }


    }
}