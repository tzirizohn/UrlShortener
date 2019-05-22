using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Url.Data
{
    public class Users
    {
        public int id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public List<URL> url { get; set; }
    }

    public class UserRepository
    {
        private string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddUser(Users u)
        {
            using (var context = new UrlContext(_connectionString))
            {
                u.Password = BCrypt.Net.BCrypt.HashPassword(u.Password);
                context.users.Add(u);
                context.SaveChanges();
            }
        }

        public Users GetByEmail(string email)
        {
            using (var context = new UrlContext(_connectionString))
            {
                return context.users.FirstOrDefault(e => e.Email == email);
            }
        }

        public bool Login(string email, string password)
        {
            var user = GetByEmail(email);
            bool verify = BCrypt.Net.BCrypt.Verify(password, user.Password);
            return verify;
        }
    }



    
}
