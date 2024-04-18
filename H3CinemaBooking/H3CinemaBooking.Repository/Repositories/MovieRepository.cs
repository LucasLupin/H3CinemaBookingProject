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
    public class MovieRepository : IMovieRepository
    {
        private readonly Dbcontext context;

        public MovieRepository(Dbcontext _context)
        {
            context = _context;
        }

        public Movie Create(Movie movie)
        {
            context.Movies.Add(movie);
            context.SaveChanges();
            return movie;
        }
        public Movie CreateComplex(Movie movie, List<string> genreNames)
        {
            context.Movies.Add(movie);

            if (movie.Genres == null)
                movie.Genres = new List<Genre>();

            foreach (string genreName in genreNames)
            {
                var genre = context.Genres.FirstOrDefault(g => g.GenreName == genreName);
                if (genre != null)
                {
                    movie.Genres.Add(genre);
                }
                else
                {
                    throw new ArgumentException($"No Genre in the database with the name: {genreName}");
                }
            }

            context.SaveChanges();
            return movie;
        }


        public Movie GetById(int Id)
        {
            var result = context.Movies.FirstOrDefault(c => c.MovieID == Id);
            return result;
        }

        //TODO: Get All Movie
        public List<Movie> GetAll()
        {
            var result = context.Movies.ToList();
            return result;
        }

        public void DeleteByID(int Id)
        {
            var movie = context.Movies.FirstOrDefault(c => c.MovieID == Id);
            if (movie != null)
            {
                context.Remove(movie);
                context.SaveChanges();
            }
        }
    }
}
