using Microsoft.AspNetCore.Mvc;
using eTickets.Data;
using Microsoft.EntityFrameworkCore;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;
        public MoviesController(IMoviesService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync(n => n.Cinema);
            return View(data);
        }

        public async Task<IActionResult> Filter(string searchString)
        {
            var data = await _service.GetAllAsync(n => n.Cinema);

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredMovie = data.Where(n => n.Name.Contains(searchString) || n.Description.Contains(searchString));
                return View("Index", filteredMovie);
            }

            return View("Index", data);
        }

        //Get: Movies/Details/id=?
        public async Task<IActionResult> Details(int id)
        {
            var data = await _service.GetMovieByIdAsync(id);
            var movie = new MovieViewModel
            {
                Id = id,
                Name = data.Name,
                Description = data.Description,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                Price = data.Price,
                ImagePath = data.ImagePath,
                MovieCategory = data.MovieCategory,
                Cinema = data.Cinema,
                Producer = data.Producer,
                Actors_Movies = data.Actors_Movies
            };
            return View(movie);
        }

        //Get: Movies/Create
        public async Task<IActionResult> Create()
        {
            var moviewDropdowns = await _service.GetMovieDropdowns();
            ViewBag.Cinemas = new SelectList(moviewDropdowns.Cinemas, "Id", "CinemaName");
            ViewBag.Producers = new SelectList(moviewDropdowns.Producers, "Id", "ProducerName");
            ViewBag.Actors = new SelectList(moviewDropdowns.Actors, "Id", "ActorName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieViewModel model)
        {
            if (!ModelState.IsValid) 
            {
                var moviewDropdowns = await _service.GetMovieDropdowns();
                ViewBag.Cinemas = new SelectList(moviewDropdowns.Cinemas, "Id", "CinemaName");
                ViewBag.Producers = new SelectList(moviewDropdowns.Producers, "Id", "ProducerName");
                ViewBag.Actors = new SelectList(moviewDropdowns.Actors, "Id", "ActorName");

                return View(model); 
            }

            await _service.AddNewMovieAsync(model);
            return RedirectToAction(nameof(Index));
        }

        //Get: Movies/Edit/id=?
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _service.GetMovieByIdAsync(id);
            if (movie == null) return View("NotFound");

            var response = new MovieViewModel()
            {
                Id = id,
                Name = movie.Name,
                Description = movie.Description,
                Price = movie.Price,
                StartDate = movie.StartDate,
                EndDate = movie.EndDate,
                ImagePath = movie.ImagePath,
                MovieCategory = movie.MovieCategory,
                CinemaId = movie.CinemaId,
                ProducerId = movie.ProducerId,
                ActorsIds = movie.Actors_Movies.Select(n => n.ActorId).ToList(),
            };

            var moviewDropdowns = await _service.GetMovieDropdowns();
            ViewBag.Cinemas = new SelectList(moviewDropdowns.Cinemas, "Id", "CinemaName");
            ViewBag.Producers = new SelectList(moviewDropdowns.Producers, "Id", "ProducerName");
            ViewBag.Actors = new SelectList(moviewDropdowns.Actors, "Id", "ActorName");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, MovieViewModel model)
        {
            if(id != model.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var moviewDropdowns = await _service.GetMovieDropdowns();
                ViewBag.Cinemas = new SelectList(moviewDropdowns.Cinemas, "Id", "CinemaName");
                ViewBag.Producers = new SelectList(moviewDropdowns.Producers, "Id", "ProducerName");
                ViewBag.Actors = new SelectList(moviewDropdowns.Actors, "Id", "ActorName");

                return View(model);
            }

            await _service.UpdateMovieAsync(model);
            return RedirectToAction(nameof(Index));
        }
    }
}
