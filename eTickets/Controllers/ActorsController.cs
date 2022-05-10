using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Entities;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ActorsController : Controller
    {
        private readonly IActorsService _service;

        public ActorsController(IActorsService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        //Get: Actors/Details/id=?
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) return View("NotFound");

            var model = new ActorViewModel
            {
                Id = id,
                ActorName = actorDetails.ActorName,
                ProfilePicturePath = actorDetails.ProfilePicturePath,
                Biography = actorDetails.Biography,
            };

            return View(model);
        }


        //Get: Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ActorName,ProfilePicturePath,Biography")]ActorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var actor = new Actor
            {
                Id = model.Id,
                ActorName = model.ActorName,
                Biography = model.Biography,
                ProfilePicturePath = model.ProfilePicturePath
            };

            await _service.AddAsync(actor);
            return RedirectToAction(nameof(Index));
        }

        //Get: Actors/Edit/id=?
        public async Task<IActionResult> Edit(int id)
        {
            var actor = await _service.GetByIdAsync(id);
            if (actor == null) return View("NotFound");
            var actorDetails = new ActorViewModel{
                Id = id,
                ActorName = actor.ActorName,
                Biography = actor.Biography,
                ProfilePicturePath = actor.ProfilePicturePath
            };
            return View(actorDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind ("Id,ActorName,ProfilePicturePath,Biography")]ActorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var actor = new Actor
            {
                Id = id,
                ActorName = model.ActorName,
                Biography = model.Biography,
                ProfilePicturePath = model.ProfilePicturePath
            };

            await _service.UpdateAsync(id, actor);
            return RedirectToAction(nameof(Index));
        }

        //Get: Actors/Delete/id=?
        public async Task<IActionResult> Delete(int id)
        {
            var actor = await _service.GetByIdAsync(id);
            if (actor == null) return View("NotFound");
            var actorDetails = new ActorViewModel
            {
                Id = id,
                ActorName = actor.ActorName,
                Biography = actor.Biography,
                ProfilePicturePath = actor.ProfilePicturePath
            };
            return View(actorDetails);
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
