using Core.Enums;

namespace BL.Services.Interfaces
{
    public interface IRoleService
    {
        public Role RoleSpecificator(string role);

        bool IsAdmin(string role);

        bool IsUser(string role);
    }
}
