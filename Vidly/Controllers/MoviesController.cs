using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {

        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Movies
        public ActionResult Random()
        {
            var movie = new Movie() { Id = 1, Name = "Shrek" };
            List<Customer> customers = new List<Customer>
            {
                new Customer{Name="Customer1"},
                new Customer{Name="Customer2"}
            };

            RandomMovieViewModel randomMovieViewModel = new RandomMovieViewModel();
            randomMovieViewModel.Movie = movie;
            randomMovieViewModel.Customers = customers;
            return View(randomMovieViewModel);
        }

        
        public ActionResult Index(int? pageIndex,string sortBy)
        {
            if (!pageIndex.HasValue)
                pageIndex = 1;

            if (String.IsNullOrWhiteSpace(sortBy))
                sortBy = "name";

            List<Movie> movies = _context.Movies.Include(c => c.Genre).ToList();
            return View(movies);

        }

        public ActionResult Detail(int id)
        {
            Movie movie = _context.Movies.Include(c => c.Genre).SingleOrDefault(c => c.Id == id);
            return View(movie);
        }

        [Route("movies/released/{year}/{month:regex(\\d{4}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year+"/"+month);
        }

        public ActionResult New()
        {
            MovieFormViewModel newMovieViewModel = new MovieFormViewModel
            {
                Genres = _context.Genres.ToList(),
                Movie = new Movie()
            };
            return View("MovieForm",newMovieViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(MovieFormViewModel newMovie)
        {
            if (!ModelState.IsValid)
            {
                MovieFormViewModel newMovieViewModel = new MovieFormViewModel
                {
                    Genres=_context.Genres.ToList(),
                    Movie=new Movie()
                };
                return View("MovieForm", newMovieViewModel);
            }

            if (newMovie.Movie.Id == 0)
            {
                _context.Movies.Add(newMovie.Movie);
            }
            else
            {
                Movie movieInDb = _context.Movies.Single(m => m.Id == newMovie.Movie.Id);

                movieInDb.DateAdded = newMovie.Movie.DateAdded;
                movieInDb.GenreId = newMovie.Movie.GenreId;
                movieInDb.NumberInStock = newMovie.Movie.NumberInStock;
                movieInDb.ReleaseDate = newMovie.Movie.ReleaseDate;
                movieInDb.Name = newMovie.Movie.Name;
            }
            _context.Movies.Add(newMovie.Movie);
            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

        public ActionResult Edit(int id)
        {
            Movie movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();

            MovieFormViewModel newMovieViewModel = new MovieFormViewModel
            {
                Genres = _context.Genres.ToList(),
                Movie = movie
            };

            return View("MovieForm",newMovieViewModel);
        }
    }
}