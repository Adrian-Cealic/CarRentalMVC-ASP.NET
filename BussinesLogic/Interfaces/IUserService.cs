using Domain;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User GetUserById(int userId);
        bool UpdateUserRole(int userId, string newRole);
    }
}