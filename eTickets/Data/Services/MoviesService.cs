using eTickets.Data.Base;
using eTickets.Entities;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        private readonly ApplicationDbContext _context;
        public MoviesService(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movie = await _context.Movies
                .Include(c => c.Cinema)
                .Include(p => p.Producer)
                .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(n => n.Id == id);
            return movie;
        }

        public async Task<MovieDropdownViewModel> GetMovieDropdowns()
        {
            var response = new MovieDropdownViewModel()
            {
                Actors = await _context.Actors.OrderBy(n => n.ActorName).ToListAsync(),
                Cinemas = await _context.Cinema.OrderBy(n => n.CinemaName).ToListAsync(),
                Producers= await _context.Producers.OrderBy(n => n.ProducerName).ToListAsync()
            };
            return response;
        }
        
        public async Task AddNewMovieAsync(MovieViewModel model)
        {
            var movie = new Movie()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                ImagePath = model.ImagePath,
                CinemaId = model.CinemaId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                MovieCategory = model.MovieCategory,
                ProducerId = model.ProducerId
            };

            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();

            foreach(var actorId in model.ActorsIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = movie.Id,
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMovieAsync(MovieViewModel model)
        {
            var dbMovie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == model.Id);

            if(dbMovie == null)
            {
                dbMovie.Name = model.Name;
                dbMovie.Description = model.Description;
                dbMovie.Price = model.Price;
                dbMovie.ImagePath = model.ImagePath;
                dbMovie.CinemaId = model.CinemaId;
                dbMovie.StartDate = model.StartDate;
                dbMovie.EndDate = model.EndDate;
                dbMovie.MovieCategory = model.MovieCategory;
                dbMovie.ProducerId = model.ProducerId;

                await _context.SaveChangesAsync();
            }

            //Remove existing actors
            var existingActorsDb = _context.Actors_Movies.Where(n => n.MovieId == model.Id).ToList();
            _context.Actors_Movies.RemoveRange(existingActorsDb);
            await _context.SaveChangesAsync();

            foreach (var actorId in model.ActorsIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = model.Id,
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }
    }
}