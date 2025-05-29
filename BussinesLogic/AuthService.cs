using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Helpers; // For password hashing
using Data_Access_Layer;
using BusinessLogic.Interfaces;

namespace BusinessLogic
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public User Authenticate(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user != null && PasswordHelper.VerifyPassword(password, user.PasswordHash))
            {
                return user;
            }
            return null;
        }

        public bool Register(string username, string password, string role = "User")
        {
            if (_context.Users.Any(u => u.Username == username))
                return false; // Username already exists

            var user = new User
            {
                Username = username,
                PasswordHash = PasswordHelper.HashPassword(password),
                Role = role
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return true;
        }
    }
}
