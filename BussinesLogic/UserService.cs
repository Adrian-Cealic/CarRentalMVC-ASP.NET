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

    }
} 