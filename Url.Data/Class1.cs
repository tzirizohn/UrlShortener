using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using shortid;


namespace Url.Data
{
    public class URL
    {
        public int Id { get; set; }
        public string LongUrl { get; set; }
        public string ShortUrl { get; set; }
        public int Count { get; set; }
        public int userid { get; set; }

        public Users users { get; set; }
                                         
    }

    public class UrlRepository
    {
        private string _connectionString;

        public UrlRepository(string connectionString)
        {
            _connectionString = connectionString;

        }

        public int AddUrl(URL u)
        {
            using (var context = new UrlContext(_connectionString))
            {        
                context.url.Add(u);               
                context.SaveChanges();
                return u.Id;
            }
        }

        public int AddUrlIfLoggedIn(URL u)
        {
            using (var context = new UrlContext(_connectionString))
            {
                List<URL>list= context.url.ToList();
                foreach (URL url in list)
                {
                    if (u.LongUrl == url.LongUrl)
                    {
                        context.Database.ExecuteSqlCommand("update url set count=@count+1 where id=@id",
                      new SqlParameter("@count", url.Count), new SqlParameter("@id", url.Id));
                        return url.Id;
                    } 
                }
                u.ShortUrl = ShortId.Generate(7);
                context.url.Add(u);
                context.SaveChanges();
                return u.Id;
            }
        }

        public URL GetShortUrl(int id)
        {
            using (var context = new UrlContext(_connectionString))
            {
                return context.url.FirstOrDefault(u => u.Id == id);
            }
        }         

        public string GetUrl(string shorturl)
        {
            using (var context = new UrlContext(_connectionString))
            {
                URL u = context.url.FirstOrDefault(i => i.ShortUrl == shorturl);
                return u.LongUrl;
            }
        }

        public IEnumerable<URL> MyUrl(int id)
        {
            using (var context = new UrlContext(_connectionString))
            {
                return context.url.Where(i => i.userid == id).ToList();
            }                                       
        }

        public bool AlreadyUsed(string longurl)
        {
            using (var context = new UrlContext(_connectionString))
            {
                return context.url.Any(u => u.LongUrl == longurl);
            }
        }
    }








    public class UrlContext : DbContext
    {
        private string _connectionString;

        public UrlContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<URL> url { get; set; }   
        public DbSet<Users> users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(_connectionString);
        }
    }

    public class UrlFactory : IDesignTimeDbContextFactory<UrlContext>
    {
        public UrlContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), $"..{Path.DirectorySeparatorChar}Homework 05-20-19"))
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();

            return new UrlContext(config.GetConnectionString("ConStr"));
        }
    }
}
