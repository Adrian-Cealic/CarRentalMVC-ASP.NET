using Data_Access_Layer;
using Domain;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Interfaces;
using BusinessLogic.Api;

namespace BusinessLogic
{
    public class UserService : UserApi, IUserService
    {
        public UserService(AppDbContext context) : base(context) { }

        public List<User> GetAllUsers()
        {
            return GetAllUsersAction();
        }

        public User GetUserById(int userId)
        {
            return GetUserByIdAction(userId);
        }

        public bool UpdateUserRole(int userId, string newRole)
        {
            return UpdateUserRoleAction(userId, newRole);
        }
    }
}