using Core.Models;
using DAL.Data;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private DBContext db;

        public UserRepository(DBContext context)
        {
            this.db = context;
        }

        public void Create(User user)
        {
            db.Users.Add(user);
        }

        public void Delete(Guid id)
        {
            User user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
            }
        }

        public User Get(Guid id)
        {
            return db.Users.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users.ToList();
        }

        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
        }

        public User Find(Guid id)
        {
            var resultData = db.Users.Where(p => p.Id == id).FirstOrDefault();
            return resultData;
        }
    }
}
