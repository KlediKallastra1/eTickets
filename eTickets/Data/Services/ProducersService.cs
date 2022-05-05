using eTickets.Data.Base;
using eTickets.Entities;

namespace eTickets.Data.Services
{
    public class ProducersService  :EntityBaseRepository<Producer>, IProducersService
    {
        public ProducersService(ApplicationDbContext context) : base(context) { }
    }
}
