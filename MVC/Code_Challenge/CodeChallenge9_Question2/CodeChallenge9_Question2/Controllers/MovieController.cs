using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeChallenge9_Question2.Models;
using CodeChallenge9_Question2.Repository;

namespace CodeChallenge9_Question2.Controllers
{
    public class MovieController : Controller
    {
        private readonly MovieRepository repo = new MovieRepository();

        public ActionResult Index()
        {
            var movies = repo.GetAllMovies();
            return View(movies);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                repo.AddMovie(movie);
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        public ActionResult Edit(int id)
        {
            var movie = repo.GetAllMovies().FirstOrDefault(m => m.Mid == id);
            return View(movie);
        }

        [HttpPost]
        public ActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                repo.UpdateMovie(movie);
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        public ActionResult Delete(int id)
        {
            repo.DeleteMovie(id);
            return RedirectToAction("Index");
        }

        public ActionResult MoviesByYear(int year)
        {
            var movies = repo.GetMoviesByYear(year);
            return View(movies);
        }

        public ActionResult MoviesByDirector(string director)
        {
            var movies = repo.GetMoviesByDirector(director);
            return View(movies);
        }
    }
}
