using H3CinemaBooking.Repository.Data;
using H3CinemaBooking.Repository.Interfaces;
using H3CinemaBooking.Repository.Models;
using H3CinemaBooking.Repository.Repositories;
using H3CinemaBooking.Repository.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace H3CinemaBooking.Test.RepositoryTest
{
    public class SeatRepository_Test
    {
        private readonly DbContextOptions<Dbcontext> options;
        private readonly Dbcontext context;
        IPropertyValidationService validationService;

        public SeatRepository_Test()
        {
            options = new DbContextOptionsBuilder<Dbcontext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            context = new Dbcontext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            validationService = new PropertyValidationService();

            // Populate initial data
            var seats = new List<Seat>
            {
                new Seat { SeatID = 2000, HallID = 1, SeatNumber = 1, SeatRow = 'A' },
                new Seat { SeatID = 2001, HallID = 1, SeatNumber = 2, SeatRow = 'A' },
                new Seat { SeatID = 2002, HallID = 2, SeatNumber = 1, SeatRow = 'A' }
            };

            context.Seats.AddRange(seats);
            context.SaveChanges();
        }

        [Fact]
        public void Create_AddsNewSeat()
        {
            // Arrange
            var repository = new SeatRepository(context, validationService);
            var newSeat = new Seat { HallID = 1, SeatNumber = 3, SeatRow = 'A' };

            // Act
            var result = repository.Create(newSeat);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.SeatNumber);
            Assert.True(context.Seats.Any(s => s.SeatNumber == 3 && s.SeatRow == 'A'));
        }

        [Fact]
        public void Create_ThrowsException_WithInvalidProperties()
        {
            // Arrange
            var repository = new SeatRepository(context, validationService);
            var invalidSeat = new Seat { SeatNumber = 3, SeatRow = 'A' };

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => repository.Create(invalidSeat));
            Assert.Equal("Invalid seat properties.", exception.Message);
        }


        [Fact]
        public void CreateBulk_AddsMultipleSeats()
        {
            // Arrange
            var repository = new SeatRepository(context, validationService);
            var newSeats = new List<Seat>
            {
                new Seat { HallID = 3, SeatNumber = 1, SeatRow = 'B' },
                new Seat { HallID = 3, SeatNumber = 2, SeatRow = 'B' }
            };

            // Act
            var results = repository.CreateBulk(newSeats);

            // Assert
            Assert.Equal(2, results.Count());
            Assert.Equal(1405, context.Seats.Count()); // Total seats after adding
        }

        [Fact]
        public void CreateBulk_WithInvalidProperties_ThrowsException()
        {
            // Arrange
            var repository = new SeatRepository(context, validationService);

            var seats = new List<Seat>
                {
                    new Seat { SeatRow = 'A', SeatNumber = 1 }, // Assume missing required properties
                    new Seat { SeatRow = 'A', SeatNumber = 2 }
                };

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => repository.CreateBulk(seats));
            Assert.Equal("Invalid seats properties.", exception.Message);
        }

        [Fact]
        public void GetById_ReturnsCorrectSeat()
        {
            // Arrange
            var repository = new SeatRepository(context, validationService);

            // Act
            var result = repository.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.SeatNumber);
        }

        [Fact]
        public void GetById_WhenNoSeatFound_ReturnsNull()
        {
            // Arrange
            var repository = new SeatRepository(context, validationService);

            // Act
            var result = repository.GetById(3000);

            // Assert
            Assert.Null(result); // Ensures method handles "not found" by returning null
        }

        [Fact]
        public void GetAll_ReturnsAllSeats()
        {
            // Arrange
            var repository = new SeatRepository(context, validationService);

            // Act
            var results = repository.GetAll();

            // Assert
            Assert.NotNull(results);
            Assert.Equal(1403, results.Count); // Total initial seats
        }

        [Fact]
        public void GetAll_WhenNoSeats_ReturnsEmptyList()
        {
            // Arrange
            var repository = new SeatRepository(context, validationService);
            context.Seats.RemoveRange(context.Seats.ToList()); // Ensure no Seat exist
            context.SaveChanges();

            // Act
            var results = repository.GetAll();

            // Assert
            Assert.NotNull(results);
            Assert.Empty(results); // Checks that an empty list is returned, not null
        }

        [Fact]
        public void GetAllSeatsFromHall_ReturnsCorrectSeats()
        {
            // Arrange
            var repository = new SeatRepository(context, validationService);

            // Act
            var results = repository.GetAllSeatsFromHall(1);

            // Assert
            Assert.Equal(202, results.Count); // Two seats in hall 1
        }

        [Fact]
        public void GetAllSeatsFromHall_WhenNoSeats_ReturnsEmptyList()
        {
            // Arrange
            var repository = new SeatRepository(context, validationService);

            var seatsInHall1 = context.Seats.Where(s => s.HallID == 1).ToList();
            context.Seats.RemoveRange(seatsInHall1);
            context.SaveChanges();

            // Act
            var results = repository.GetAllSeatsFromHall(1);

            // Assert
            Assert.NotNull(results);  
            Assert.Empty(results); 
        }

        [Fact]
        public void DeleteByID_RemovesSeat()
        {
            // Arrange
            var repository = new SeatRepository(context, validationService);

            // Act
            var result = repository.DeleteByID(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void DeleteByID_Throw_Exception()
        {
            // Arrange
            var repository = new SeatRepository(context, validationService);

            // Act
            var result = repository.DeleteByID(3000);

            // Assert
            Assert.False(result);
        }
    }
}
