using Core.Models;
using DAL.Data;
using DAL.Interfaces;
using System;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private DBContext _db;
        private UserRepository userRepository;
        private MaterialRepository materialRepository;

        public UnitOfWork()
        {
            _db = new DBContext();
        }

        public IUserRepository Users
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(_db);
                }
                return userRepository;
            }
        }

        public IMaterialRepository Materials
        {
            get
            {
                if (materialRepository == null)
                {
                    materialRepository = new MaterialRepository(_db);
                }
                return materialRepository;
            }
        }

        public Task<int> SaveAsync()
        {
            return _db.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
