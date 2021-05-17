using System;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IMaterialRepository Materials { get; }
        Task<int> SaveAsync();
    }
}
