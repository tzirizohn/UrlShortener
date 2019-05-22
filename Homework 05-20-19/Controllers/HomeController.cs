using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Homework_05_20_19.Models;
using Url.Data;
using Microsoft.Extensions.Configuration;
using shortid;
using Microsoft.AspNetCore.Authorization;

namespace Homework_05_20_19.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString;

        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                UserRepository us = new UserRepository(_connectionString);   
                Users u = us.GetByEmail(User.Identity.Name);
                return View(u);    
            }
            return View(); 
        } 
        
        public IActionResult AddUrl(string longurl)
        {
            UrlRepository ur = new UrlRepository(_connectionString);
            UserRepository us = new UserRepository(_connectionString);
            URL u = new URL();                               
            u.LongUrl = longurl;
            
            if(User.Identity.IsAuthenticated)
            {
                u.userid = us.GetByEmail(User.Identity.Name).id;
                int id= ur.AddUrlIfLoggedIn(u);
                URL url = ur.GetShortUrl(id);  
                return Json(url.ShortUrl);
            }
            else
            {
                u.ShortUrl = ShortId.Generate(7);
                int id = ur.AddUrl(u);
                URL url = ur.GetShortUrl(id);    
                return Json(url.ShortUrl);
            }     
        }

        [Route("{shorturl}")]
        public IActionResult GetUrl(string shorturl)
        {
            UrlRepository ur = new UrlRepository(_connectionString);
            string longurl = ur.GetUrl(shorturl);
            return Redirect(longurl);
        }

        [Authorize]
        public IActionResult MyURl(int id)
        {
            UrlRepository ur = new UrlRepository(_connectionString);
            IEnumerable<URL> list = ur.MyUrl(id);
            return View(list);
        }

    }
}
