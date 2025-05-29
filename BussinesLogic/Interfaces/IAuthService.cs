using Domain;

namespace BusinessLogic.Interfaces
{
    public interface IAuthService
    {
        User Authenticate(string username, string password);
        bool Register(string username, string password, string role);
    }
}