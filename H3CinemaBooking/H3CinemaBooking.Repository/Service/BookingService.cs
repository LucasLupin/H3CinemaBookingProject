using H3CinemaBooking.Repository.Interfaces;
using H3CinemaBooking.Repository.Models;
using H3CinemaBooking.Repository.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3CinemaBooking.Repository.Service
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepositry;
        public BookingService(IBookingRepository bookingRepositry)
        {
            _bookingRepositry = bookingRepositry;
        }

        public void ReserveSeats(ReserveSeatDTO reserveSeat)
        {
            Booking booking = new Booking();
            booking.ShowID = reserveSeat.ShowID;
            booking.UserDetailID = reserveSeat.UserID;
            booking.NumberOfSeats = reserveSeat.SeatList.Count;
            booking.BookingSeats = new List<BookingSeat>();
            booking.Price = reserveSeat.Price * booking.NumberOfSeats;

            //TODO: Check if any of the seats is already booked

            //Create booking in database
            //Check if the seats already is booked
            var bookingCreatedObjekt = _bookingRepositry.Create(booking);

            // For each seat in seatlist create a booking seat
            for (int i = 0; i < reserveSeat.SeatList.Count; i++)
            {
                var currentSeat = reserveSeat.SeatList[i];
                BookingSeat bookingSeat = new BookingSeat();
                bookingSeat.SeatID = currentSeat.SeatID;
                bookingSeat.BookingID = bookingCreatedObjekt.BookingID;

                // Try to parse the SeatStatus string to SeatStatus enum
                if (Enum.TryParse<SeatStatus>(currentSeat.SeatStatus, true, out SeatStatus status))
                {
                    bookingSeat.Status = status;
                }
                else
                {
                    Console.WriteLine("Invalid seat status value: " + currentSeat.SeatStatus);
                }

                booking.BookingSeats.Add(bookingSeat);
            }
            bookingCreatedObjekt.BookingSeats = booking.BookingSeats;
            var bookingSeatsCreated = _bookingRepositry.CreateBookingSeat(bookingCreatedObjekt);


            // Additional logic here to save the booking to a database, if necessary
        }


    }
}
