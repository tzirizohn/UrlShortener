using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Url.Data;

namespace Homework_05_20_19.Controllers
{                                          
    public class AccountController : Controller
    {
        private string _connectionString;

        public AccountController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignUpForm()
        {
            return View();
        }

        public IActionResult SignUp(Users user)
        {
            UserRepository us = new UserRepository(_connectionString);
            us.AddUser(user);
            return Redirect("/");
        }

        public IActionResult LoginForm()
        {
            return View();
        }

        public IActionResult Login(string Email, string Password)
        {
            UserRepository ur = new UserRepository(_connectionString);
            bool isVerified = ur.Login(Email, Password);
            if (isVerified)
            {
                var claims = new List<Claim>
                {
                    new Claim("user", Email)
                };
                HttpContext.SignInAsync(new ClaimsPrincipal(
                    new ClaimsIdentity(claims, "Cookies", "user", "role"))).Wait();

                return Redirect("/home/index");
            }
            else
            {
                return Redirect("/home/loginform");
            }
        }

    }
}