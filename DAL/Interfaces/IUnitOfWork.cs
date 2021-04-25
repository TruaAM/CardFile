using Core.Models;
using System;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Material> Materials { get; }
        void Save();
    }
}
