using System.Collections.Generic;
using System.Linq;
using CodeChallenge9_Question2.Models;

namespace CodeChallenge9_Question2.Repository
{
    public class MovieRepository
    {
        private readonly MovieContext _context;

        public MovieRepository()
        {
            _context = new MovieContext();
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return _context.Movies.ToList();
        }

        public void AddMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
        }

        public void UpdateMovie(Movie movie)
        {
            _context.Entry(movie).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteMovie(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Movie> GetMoviesByYear(int year)
        {
            return _context.Movies.Where(m => m.DateOfRelease.Year == year).ToList();
        }

        public IEnumerable<Movie> GetMoviesByDirector(string director)
        {
            return _context.Movies.Where(m => m.DirectorName == director).ToList();
        }
    }
}
