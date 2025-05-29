using Data_Access_Layer;
using Domain;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Api
{
    public class UserApi
    {
        protected readonly AppDbContext _context;

        public UserApi(AppDbContext context)
        {
            _context = context;
        }

        internal List<User> GetAllUsersAction()
        {
            return _context.Users.ToList();
        }

        internal User GetUserByIdAction(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.Id == userId);
        }

        internal bool UpdateUserRoleAction(int userId, string newRole)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                // Basic validation for role, you might want to make this more robust
                // e.g., by checking against a list of valid roles or an enum
                if (!string.IsNullOrWhiteSpace(newRole) && (newRole == "Admin" || newRole == "User"))
                {
                    user.Role = newRole;
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}