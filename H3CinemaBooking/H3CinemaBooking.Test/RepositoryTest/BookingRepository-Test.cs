using H3CinemaBooking.Repository.Data;
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
            context.SaveChanges();
        }

        [Fact]
        public void GetById_ChecksNullAndValidData()
        {
                // Arrange
                var repository = new BookingRepository(context);

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
            var repository = new BookingRepository(context);

            // Act
            
            var Booking = repository.Create(new Booking() { ShowID = 4, UserDetailID = 4, NumberOfSeats = 200, Price = 130, IsActive = true });

            // Assert
            // Check for valid data
            Assert.NotNull(Booking);



        }
    }
}
