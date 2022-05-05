using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Entities;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICinemasService _service;

        public CinemasController(ICinemasService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        //Get: Cinemas/Details/id=?
        public async Task<IActionResult> Details(int id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null) return View("NotFound");
            var cinema = new CinemaViewModel
            {
                Id = id,
                CinemaName = data.CinemaName,
                LogoPath = data.LogoPath,
                Description = data.Description,
            };
            return View(cinema);
        }

        //Get: Cinemas/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,CinemaName,LogoPath,Description")] CinemaViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var cinema = new Cinema
            {
                Id = model.Id,
                CinemaName = model.CinemaName,
                Description = model.Description,
                LogoPath = model.LogoPath,
            };
            await _service.AddAsync(cinema);
            return RedirectToAction(nameof(Index));
        }

        //Get: Cinemas/Edit/id=?
        public async Task<IActionResult> Edit(int id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null) return View("NotFound");
            var cinema = new CinemaViewModel
            {
                Id = id,
                CinemaName = data.CinemaName,
                LogoPath = data.LogoPath,
                Description = data.Description,
            };
            return View(cinema);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CinemaName,LogoPath,Description")] CinemaViewModel model)
        {
            if(!ModelState.IsValid) return View(model);

            if(id != model.Id) return View("NotFound");

            var cinema = new Cinema
            {
                Id = model.Id,
                CinemaName = model.CinemaName,
                LogoPath = model.LogoPath,
                Description = model.Description,
            };

            await _service.UpdateAsync(id, cinema);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null) return View("NotFound");
            var cinema = new CinemaViewModel
            {
                Id = id,
                CinemaName = data.CinemaName,
                LogoPath = data.LogoPath,
                Description = data.Description
            };
            return View(cinema);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cinema = await _service.GetByIdAsync(id);
            if (cinema == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
