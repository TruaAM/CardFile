using Core.Models;
using System;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IMaterialRepository Materials { get; }
        Task<int> SaveAsync();
    }
}
