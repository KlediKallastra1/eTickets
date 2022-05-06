using eTickets.Data.Base;
using eTickets.Entities;
using eTickets.Models;

namespace eTickets.Data.Services
{
    public interface IMoviesService : IEntityBaseRepository<Movie>
    {
        public Task<Movie> GetMovieByIdAsync(int id);
        public Task<MovieDropdownViewModel> GetMovieDropdowns();
        public Task AddNewMovieAsync(MovieViewModel model);
        public Task UpdateMovieAsync(MovieViewModel model);
    }
}
