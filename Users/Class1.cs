using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Url.Data;

namespace Users
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
            using (var context = new SignalContext(_connectionString))
            {
                u.Password = BCrypt.Net.BCrypt.HashPassword(u.Password);
                context.Users.Add(u);
                context.SaveChanges();
            }
        }

        public Users GetByEmail(string email)
        {
            using (var context = new SignalContext(_connectionString))
            {
                return context.Users.FirstOrDefault(e => e.Email == email);
            }
        }

        public bool Login(string email, string password)
        {
            var user = GetByEmail(email);
            bool verify = BCrypt.Net.BCrypt.Verify(password, user.Password);
            return verify;
        }
    }



    public class SignalContext : DbContext
    {
        private string _connectionString;

        public SignalContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(_connectionString);
        }
    }

    public class SignalFactory : IDesignTimeDbContextFactory<SignalContext>
    {
        public SignalContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), $"..{Path.DirectorySeparatorChar}Homework 05-20-19"))
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();

            return new SignalContext(config.GetConnectionString("ConStr"));
        }
    }
}
