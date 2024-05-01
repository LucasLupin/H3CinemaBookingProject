using H3CinemaBooking.Repository.Data;
using H3CinemaBooking.Repository.Interfaces;
using H3CinemaBooking.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3CinemaBooking.Repository.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly Dbcontext context;

        public GenreRepository(Dbcontext _context)
        {
            context = _context;
        }
        public Genre Create(Genre Genre)
            {
            context.Genres.Add(Genre);
            context.SaveChanges();
            return Genre;
        }
        public Genre GetById(int Id)
        {
            var result = context.Genres.FirstOrDefault(c => c.GenreID == Id);
            return result;
        }
        public List<Genre> GetAll()
        {
            var result = context.Genres.ToList();
            return result;
        }

        public void UpdateByID(int Id, Genre updatedGenre)
        {
            var genre = context.Genres.FirstOrDefault(g => g.GenreID == Id);

            if (genre != null)
            {
                genre.GenreName = updatedGenre.GenreName;

                context.SaveChanges();
            }
        }

        public void DeleteGenreByID(int Id)
        {
            var genre = context.Genres.FirstOrDefault(c => c.GenreID == Id);
            if (genre != null)
            {
                context.Remove(genre);
                context.SaveChanges();
            }
        }
    }
}
