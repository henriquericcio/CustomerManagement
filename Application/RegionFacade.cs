using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagement.Infrastructure;
using CustomerManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Application.Contracts
{
    public class RegionFacade : IRegionFacade
    {
        private readonly CustomerManagementContext _context;

        public RegionFacade(CustomerManagementContext context)
        {
            _context = context;
        }
        public Task<IEnumerable<Region>> Get()
        {
            return _context.Regions.ToListAsync().ContinueWith(l=>l.Result.AsEnumerable());
        }
    }
}