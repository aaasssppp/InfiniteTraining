using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CodeChallenge9_Question2.Models
{
    public class MovieContext : DbContext
    {
        public MovieContext() : base("MoviesDb") { }

        public DbSet<Movie> Movies { get; set; }
    }
}