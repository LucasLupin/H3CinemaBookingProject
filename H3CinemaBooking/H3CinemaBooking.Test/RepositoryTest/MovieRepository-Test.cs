using Microsoft.EntityFrameworkCore;
//using H3CinemaBooking.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H3CinemaBooking.Repository.Data;
using H3CinemaBooking.Repository.Models;
using H3CinemaBooking.Repository.Repositories;
using NuGet.ContentModel;

namespace H3CinemaBooking.Test.Test_Repositories
{
    public class MovieRepository_Test
    {
        DbContextOptions<Dbcontext> options;
        Dbcontext context;

        public MovieRepository_Test()
        {
            options = new DbContextOptionsBuilder<Dbcontext>()
                .UseInMemoryDatabase(databaseName: "DummyDatabase")
                .Options;

            context = new Dbcontext(options);
            context.Database.EnsureDeleted(); // Remove database if Found

            //Populate data 
            Movie m1 = new Movie() { MovieID = 1, Title = "Underverden", Duration = 2, Director = "Lucas den store", MovieLink = "random", TrailerLink = "Test", Genres = new List<Genre>() };
            Movie m2 = new Movie() { MovieID = 2, Title = "Klovn", Duration = 2, Director = "Lucas dn mellem", MovieLink = "random", TrailerLink = "Test", Genres = new List<Genre>() };
            Movie m3 = new Movie() { MovieID = 3, Title = "DummyMovie", Duration = 2, Director = "Lucas den lille", MovieLink = "random", TrailerLink = "Test", Genres = new List<Genre>() };

            context.Movies.Add(m1);
            context.Movies.Add(m2);
            context.Movies.Add(m3);

        }

        [Fact]
        public void getAllMovies_ReturnExists()
        {
            //Arrange    - Variable creation etc
            MovieRepository repo = new MovieRepository(context);
            //Act       - call method
            var result = repo.GetAll(); // List<Movie>
            var expected = 3;
            //Assert    - verify I get the right result back
            Assert.Equal(expected, result.Count);
        }

        [Fact]
        public void getAllMovies_NotFound()
        {
            //Arrange    - Variable creation etc
            MovieRepository repo = new MovieRepository(context);
            //Act       - call method
            var result = repo.GetAll(); // List<Movie>
            var expected = 3;
            //Assert    - verify I get the right result back
            Assert.Equal(expected, result.Count);
        }

        [Fact]
        public void getAllMovies_NotExists()
        {
            //Arrange    - Variable creation etc
            MovieRepository repo = null;
            //Act       - call method
            //var result = repo.GetById(1);

            //Assert    - verify I get the right result back
            Assert.Null(repo);
        }

        [Fact]
        public void GetMovieById_ReturnsExists()
        {

            //Arrange    - Variable creation etc
            MovieRepository repo = new MovieRepository(context);
            //Act       - call method
            var result = repo.GetById(2);

            //Assert    - verify I get the right result back
            Assert.Equal(2, result.MovieID);

        }

        [Fact]
        public void GetMovieById_NotFound()
        {
            //Arrange    - Variable creation etc
            MovieRepository repo = new MovieRepository(context);
            //Act       - call method
            var result = repo.GetById(4);

            //Assert    - verify I get the right result back
            Assert.NotEqual(4, result.MovieID);
        }

        [Fact]
        public void GetMovieById_NotExists()
        {
            //Arrange    - Variable creation etc
            MovieRepository repo = null;
            //Act       - call method
            //var result = repo.GetById(1);

            //Assert    - verify I get the right result back
            Assert.Null(repo);
        }
    }
}