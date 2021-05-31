using Core.Models;
using DAL.Data;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    /// <summary>
    /// This repository has basic functionallity to manipulate with users-table in database
    /// It is needed to incapsulate logic of working with source of data
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private DBContext db;

        public UserRepository(DBContext context)
        {
            this.db = context;
        }

        public Task CreateAsync(User user)
        {
            return Task.FromResult(db.Users.Add(user));
        }

        public void Delete(User user)
        {
            db.Users.Remove(user);
        }

        public Task DeleteByIdAsync(Guid id)
        {
            User user = db.Users.Find(id);
            if (user != null)
            {
                return Task.FromResult(db.Users.Remove(user));
            }
            return null;
        }

        public Task<User> GetAsync(Guid id)
        {
            return Task.FromResult(db.Users.Find(id));
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users.ToList();
        }

        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
        }

        public Task<User> FindAsync(Guid id)
        {
            var resultData = db.Users.Where(p => p.Id == id).FirstOrDefault();
            return Task.FromResult(resultData);
        }
    }
}
