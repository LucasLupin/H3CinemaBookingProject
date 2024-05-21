using H3CinemaBooking.API.Controllers;
using H3CinemaBooking.Repository.Interfaces;
using H3CinemaBooking.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace H3CinemaBooking.Test.ControllerTest
{
    public class ShowController_Test
    {
        private readonly Mock<IShowRepository> _mockRepo;
        private readonly IShowService _showService;
        private readonly ShowController _controller;

        public ShowController_Test(IShowService showService)
        {
            _mockRepo = new Mock<IShowRepository>();
            _showService = showService;
            _controller = new ShowController(_mockRepo.Object, _showService);
        }

        [Fact]
        public void GetAll_ReturnsOk_WhenShowExist()
        {
            // Arrange
            DateTime currentDateTime = DateTime.Now;
            //Make a new list of booking
            var shows = new List<Show>
            {
                new Show { ShowID = 1, HallID = 1, MovieID = 1, Price = 135, ShowDateTime = currentDateTime, Bookings = new List<Booking>() },
                new Show { ShowID = 2, HallID = 1, MovieID = 2, Price = 150, ShowDateTime = currentDateTime, Bookings = new List<Booking>() }
            };
            _mockRepo.Setup(repo => repo.GetAll()).Returns(shows);

            // Act
            var result = _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Movie>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        //[Fact]
        //public void GetAll_ReturnsNoContent_WhenNoMoviesExist()
        //{
        //    // Arrange
        //    _mockRepo.Setup(repo => repo.GetAll()).Returns(new List<Movie>());

        //    // Act
        //    var result = _controller.GetAll();

        //    // Assert
        //    Assert.IsType<NoContentResult>(result.Result);
        //}

        //[Fact]
        //public void GetByID_ReturnsOk_WhenMovieExists()
        //{
        //    // Arrange
        //    var movie = new Movie { MovieID = 1, Title = "TestMovie", Duration = 120, Director = "Director", MovieLink = "Link", TrailerLink = "Trailer" };
        //    _mockRepo.Setup(repo => repo.GetById(1)).Returns(movie);

        //    // Act
        //    var result = _controller.GetByID(1);

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result.Result);
        //    var returnValue = Assert.IsType<Movie>(okResult.Value);
        //    Assert.Equal(1, returnValue.MovieID);
        //}

        //[Fact]
        //public void GetByID_ReturnsNotFound_WhenMovieDoesNotExist()
        //{
        //    // Arrange
        //    _mockRepo.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((Movie)null);

        //    // Act
        //    var result = _controller.GetByID(100);

        //    // Assert
        //    Assert.IsType<NotFoundResult>(result.Result);
        //}

        //[Fact]
        //public void Post_ReturnsOk_WhenMovieIsCreated()
        //{
        //    // Arrange
        //    var movie = new Movie { MovieID = 1, Title = "TestMovie", Duration = 120, Director = "Director", MovieLink = "Link", TrailerLink = "Trailer" };
        //    _mockRepo.Setup(repo => repo.Create(movie));

        //    // Act
        //    var result = _controller.Post(movie);

        //    // Assert
        //    var actionResult = Assert.IsType<ActionResult<Movie>>(result);
        //    var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        //    var returnValue = Assert.IsType<string>(okResult.Value);
        //    Assert.Equal("Movie created successfully.", returnValue);
        //}

        //[Fact]
        //public void Post_ReturnsBadRequest_WhenMovieIsNull()
        //{
        //    // Act
        //    var result = _controller.Post(null);

        //    // Assert
        //    var actionResult = Assert.IsType<ActionResult<Movie>>(result);
        //    var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        //    Assert.Equal("Movie data is required", badRequestResult.Value);
        //}

        //[Fact]
        //public void PostComplex_ReturnsOk_WhenMovieIsCreated()
        //{
        //    // Arrange
        //    var movie = new Movie
        //    {
        //        MovieID = 1,
        //        Title = "TestMovie",
        //        Duration = 120,
        //        Director = "Director",
        //        MovieLink = "Link",
        //        TrailerLink = "Trailer",
        //        Genres = new List<Genre> { new Genre { GenreName = "Action" } }
        //    };
        //    var createdMovie = new Movie { MovieID = 1, Title = "TestMovie", Duration = 120, Director = "Director", MovieLink = "Link", TrailerLink = "Trailer" };
        //    _mockRepo.Setup(repo => repo.CreateComplex(It.IsAny<Movie>(), It.IsAny<List<string>>())).Returns(createdMovie);

        //    // Act
        //    var result = _controller.PostComplex(movie);

        //    // Assert
        //    var actionResult = Assert.IsType<ActionResult<Movie>>(result);
        //    var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        //    var returnValue = Assert.IsType<Movie>(okResult.Value);
        //    Assert.Equal(1, returnValue.MovieID);
        //}

        //[Fact]
        //public void PostComplex_ReturnsBadRequest_WhenMovieIsNull()
        //{
        //    // Act
        //    var result = _controller.PostComplex(null);

        //    // Assert
        //    var actionResult = Assert.IsType<ActionResult<Movie>>(result);
        //    var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        //    Assert.Equal("Movie data is required", badRequestResult.Value);
        //}

        //[Fact]
        //public void PostComplex_ReturnsBadRequest_OnException()
        //{
        //    // Arrange
        //    var movie = new Movie { MovieID = 1, Title = "TestMovie", Duration = 120, Director = "Director", MovieLink = "Link", TrailerLink = "Trailer" };
        //    _mockRepo.Setup(repo => repo.CreateComplex(It.IsAny<Movie>(), It.IsAny<List<string>>())).Throws(new ArgumentException("Error"));

        //    // Act
        //    var result = _controller.PostComplex(movie);

        //    // Assert
        //    var actionResult = Assert.IsType<ActionResult<Movie>>(result);
        //    var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        //    Assert.Equal("Error", badRequestResult.Value);
        //}

        //[Fact]
        //public void Update_ReturnsOk_WhenMovieIsUpdated()
        //{
        //    // Arrange
        //    var movie = new Movie { MovieID = 1, Title = "UpdatedMovie", Duration = 120, Director = "Director", MovieLink = "Link", TrailerLink = "Trailer" };
        //    var existingMovie = new Movie { MovieID = 1, Title = "TestMovie", Duration = 90, Director = "Director", MovieLink = "Link", TrailerLink = "Trailer" };
        //    _mockRepo.Setup(repo => repo.GetById(1)).Returns(existingMovie);

        //    // Act
        //    var result = _controller.Update(1, movie);

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    var returnValue = Assert.IsType<Movie>(okResult.Value);
        //    Assert.Equal("UpdatedMovie", returnValue.Title);
        //}

        //[Fact]
        //public void Update_ReturnsNotFound_WhenMovieDoesNotExist()
        //{
        //    // Arrange
        //    _mockRepo.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((Movie)null);

        //    // Act
        //    var result = _controller.Update(1, new Movie { MovieID = 1 });

        //    // Assert
        //    Assert.IsType<NotFoundResult>(result);
        //}

        //[Fact]
        //public void Update_ReturnsBadRequest_WhenMovieIsNull()
        //{
        //    // Act
        //    var result = _controller.Update(1, null);

        //    // Assert
        //    Assert.IsType<BadRequestResult>(result);
        //}

        //[Fact]
        //public void Delete_ReturnsOk_WhenMovieIsDeleted()
        //{
        //    // Arrange
        //    _mockRepo.Setup(repo => repo.DeleteByID(1)).Returns(true);

        //    // Act
        //    var result = _controller.Delete(1);

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    var returnValue = okResult.Value.GetType().GetProperty("message").GetValue(okResult.Value, null);
        //    Assert.Equal("Movie deleted successfully", returnValue);
        //}

        //[Fact]
        //public void Delete_ReturnsNotFound_WhenMovieDoesNotExist()
        //{
        //    // Arrange
        //    _mockRepo.Setup(repo => repo.DeleteByID(It.IsAny<int>())).Returns(false);

        //    // Act
        //    var result = _controller.Delete(1);

        //    // Assert
        //    var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        //    Assert.Equal("ID was not found!", notFoundResult.Value);
        //}
    }
}
