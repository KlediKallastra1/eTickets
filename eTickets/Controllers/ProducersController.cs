using Microsoft.AspNetCore.Mvc;
using eTickets.Data;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using eTickets.Data.Services;
using eTickets.Entities;

namespace eTickets.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IProducersService _service;

        public ProducersController(IProducersService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        //Get: Producers/Details/id=?
        public async Task<IActionResult> Details(int id)
        {
            var prodDetails = await _service.GetByIdAsync(id);
            if (prodDetails == null) return View("NotFound");
            var model = new ProducerViewModel
            {
                Id = id,
                ProducerName = prodDetails.ProducerName,
                ProfilePicturePath = prodDetails.ProfilePicturePath,
                Biography = prodDetails.Biography,
            };
            return View(model);
        }

        //Get: Producers/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProducerName,ProfilePicturePath,Biography")] ProducerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var producers = new Producer
            {
                Id = model.Id,
                ProducerName = model.ProducerName,
                Biography = model.Biography,
                ProfilePicturePath = model.ProfilePicturePath,
            };
            await _service.AddAsync(producers);
            return RedirectToAction(nameof(Index));
        }

        //Get: Producers/Edit/id=?
        public async Task<IActionResult> Edit(int id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null) return View("NotFound");
            var model = new ProducerViewModel
            {
                Id = data.Id,
                ProducerName = data.ProducerName,
                ProfilePicturePath = data.ProfilePicturePath,
                Biography = data.Biography,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProducerName,ProfilePicturePath,Biography")] ProducerViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            if(id == model.Id)
            {
                var producer = new Producer
                {
                    Id = model.Id,
                    ProducerName = model.ProducerName,
                    ProfilePicturePath = model.ProfilePicturePath,
                    Biography = model.Biography,
                };

                await _service.UpdateAsync(id, producer);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null) return View("NotFound");
            var producer = new ProducerViewModel
            {
                Id = id,
                ProducerName = data.ProducerName,
                Biography = data.Biography,
                ProfilePicturePath = data.ProfilePicturePath
            };
            return View(producer);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actor = await _service.GetByIdAsync(id);
            if (actor == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
