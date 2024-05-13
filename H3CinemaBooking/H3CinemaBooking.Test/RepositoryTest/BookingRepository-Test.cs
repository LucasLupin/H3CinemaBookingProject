using H3CinemaBooking.Repository.Data;
using H3CinemaBooking.Repository.Interfaces;
using H3CinemaBooking.Repository.Models;
using H3CinemaBooking.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3CinemaBooking.Test.RepositoryTest
{
    public class BookingRepository_Test
    {
        DbContextOptions<Dbcontext> options;
        Dbcontext context;
        IPropertyValidationService validationService;

        public BookingRepository_Test()
        {
            options = new DbContextOptionsBuilder<Dbcontext>()
                .UseInMemoryDatabase(databaseName: "DummyDatabase")
                .Options;

            context = new Dbcontext(options);
            context.Database.EnsureDeleted(); // Remove database if Found

            //Populate data 
            Booking b1 = new Booking() { BookingID = 1, ShowID = 1, UserDetailID = 1, NumberOfSeats = 200, Price = 130, IsActive = true};
            Booking b2 = new Booking() { BookingID = 2, ShowID = 2, UserDetailID = 2, NumberOfSeats = 200, Price = 130, IsActive = true };
            Booking b3 = new Booking() { BookingID = 3, ShowID = 3, UserDetailID = 3, NumberOfSeats = 200, Price = 130, IsActive = true };

            context.Bookings.Add(b1);
            context.Bookings.Add(b2);
            context.Bookings.Add(b3);

            // Add BookingSeat entries
            context.BookingSeats.Add(new BookingSeat { BookingSeatID = 1, BookingID = 1, SeatID = 1 });
            context.BookingSeats.Add(new BookingSeat { BookingSeatID = 2, BookingID = 1, SeatID = 2 });
            context.BookingSeats.Add(new BookingSeat { BookingSeatID = 3, BookingID = 2, SeatID = 3 });

            context.SaveChanges();
        }

        [Fact]
        public void GetById_ChecksNullAndValidData()
        {
                // Arrange
                var repository = new BookingRepository(context, validationService);

                // Act
                var validBooking = repository.GetById(1);
                var nullBooking = repository.GetById(4);

                // Assert
                // Check for valid data
                Assert.NotNull(validBooking);
                Assert.Equal(1, validBooking.BookingID);

                // Check for null scenario
                Assert.Null(nullBooking);
        }

        [Fact]
        public void CreateBooking_IfExist()
        {
            // Arrange
            var repository = new BookingRepository(context, validationService);

            // Act

            var booking = new Booking() { ShowID = 4, UserDetailID = 4, NumberOfSeats = 200, Price = 130, IsActive = true};

            var result = repository.Create(booking);

            // Assert
            // Check for valid data
            Assert.NotNull(result);

        }


        [Fact]
        public void DeleteBooking_WhenExists()
        {
            //Arrange
            BookingRepository repository = new BookingRepository(context, validationService);
            //Act - Method calling
            var result = repository.DeleteByID(1);
            Assert.True(result);
        }

        [Fact]
        public void GetBookingSeatsByShowId_Returns_CorrectSeats()
        {
            // Arrange
            var repository = new BookingRepository(context, validationService);

            // Act
            var seatsForShow1 = repository.GetBookingSeatsByShowId(1); // Assuming seats exist for ShowID 1

            // Assert
            Assert.NotNull(seatsForShow1);
            Assert.Equal(2, seatsForShow1.Count); // Expecting 2 seats for ShowID 1
        }

        [Fact]
        public void GetBookingSeatsByShowId_ThrowsException_WhenNoSeats()
        {
            // Arrange
            var repository = new BookingRepository(context, validationService);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => repository.GetBookingSeatsByShowId(99)); // Assuming show ID 99 has no seats
            Assert.Equal("No booking seats found for show ID 99.", exception.Message);
        }

        [Fact]
        public void CreateBookingSeat_ValidInput_CreatesSeats()
        {
            // Arrange
            var repository = new BookingRepository(context, validationService);
            var booking = new Booking
            {
                BookingID = 4,  // Assuming a new booking ID not yet used
                BookingSeats = new List<BookingSeat>
                {
                new BookingSeat { SeatID = 4, BookingID = 4, Status = SeatStatus.Reserved }  // New seat for the booking
                }
            };

            // Act
            var result = repository.CreateBookingSeat(booking);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(4, result.First().SeatID);
        }

        [Fact]
        public void CreateBookingSeat_NoSeats_ThrowsException()
        {
            // Arrange
            var repository = new BookingRepository(context, validationService);
            var booking = new Booking
            {
                BookingID = 5,
                BookingSeats = new List<BookingSeat>()  // Empty list of booking seats
            };

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => repository.CreateBookingSeat(booking));
            Assert.Equal("Booking must have at least one seat.", exception.Message);
        }



    }
}
