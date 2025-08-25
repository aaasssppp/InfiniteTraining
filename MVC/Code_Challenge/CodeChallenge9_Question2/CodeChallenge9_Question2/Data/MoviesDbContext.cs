using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using CodeChallenge9_Question2.Models;

namespace CodeChallenge9_Question2.Data
{
    public class MoviesDbContext : DbContext
    {
        // Name matches the connection string key in Web.config
        public MoviesDbContext() : base("MoviesDbContext") { }

        public DbSet<Movie> Movies { get; set; }
    }
}