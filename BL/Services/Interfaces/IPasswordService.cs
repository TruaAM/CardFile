using Core.Enums;

namespace BL.Services.Interfaces
{
    public interface IPasswordService
    {
        public PasswordStrength CheckPasswordStrength(string password);

        public string GetHashString(string password);

        bool IsPasswordStrong(string password);
    }
}
