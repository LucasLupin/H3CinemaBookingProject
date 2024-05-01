using H3CinemaBooking.Repository.Data;
using H3CinemaBooking.Repository.Interfaces;
using H3CinemaBooking.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace H3CinemaBooking.Repository.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly Dbcontext context;
        private readonly DbSet<TEntity> dbSet;

        public GenericRepository(Dbcontext _context)
        {
            context = _context;
            dbSet = context.Set<TEntity>();
        }

        public TEntity Create(TEntity entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public TEntity GetById(int id)
        {
            return dbSet.Find(id);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
             return await dbSet.ToListAsync();
        }

        public interface IEntity
        {
            int Id { get; set; }
        }

        public void Update(Cinema cinema)
        {
            context.Entry(cinema).State = EntityState.Modified;
            context.SaveChanges();
        }



        public void DeleteById(int id)
        {
            var entity = dbSet.Find(id);
            if (entity != null)
            {
                dbSet.Remove(entity);
                context.SaveChanges();
            }
        }

    }
}
