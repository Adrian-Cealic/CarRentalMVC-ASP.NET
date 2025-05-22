using Data_Access_Layer;
using Domain;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserById(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.Id == userId);
        }

        public bool UpdateUserRole(int userId, string newRole)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                // Basic validation for role, you might want to make this more robust
                // e.g., by checking against a list of valid roles or an enum
                if (!string.IsNullOrWhiteSpace(newRole) && (newRole == "Admin" || newRole == "User")) // Example roles
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