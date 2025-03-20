using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Helpers; // For password hashing

namespace BusinessLogic
{
    public class AuthService
    {
        private static readonly List<User> _users = new List<User>
        {
            new User { Id = 1, Username = "admin", PasswordHash = PasswordHelper.HashPassword("admin123"), Role = "Admin" },
            new User { Id = 2, Username = "user", PasswordHash = PasswordHelper.HashPassword("user123"), Role = "User" }
        };

        public User Authenticate(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.Username == username);
            if (user != null && PasswordHelper.VerifyPassword(password, user.PasswordHash))
            {
                return user;
            }
            return null; 
        }
    }
}
