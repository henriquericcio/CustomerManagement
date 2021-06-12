using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagement.Application.Contracts;
using CustomerManagement.Application.Contracts.Dto;
using CustomerManagement.Infrastructure;
using CustomerManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Application
{
    public class RegionFacade : IRegionFacade
    {
        private readonly CustomerManagementContext _context;

        public RegionFacade(CustomerManagementContext context)
        {
            _context = context;
        }
        public Task<IEnumerable<RegionDto>> Get()
        {
            return _context.Regions.Select(r=> new RegionDto{ Cities = r.Cities.Select(c=> new CityDto{Id = c.Id,Name = c.Name}), Id = r.Id, Name = r.Name}).ToListAsync<RegionDto>().ContinueWith(l=>l.Result.AsEnumerable());
        }
    }
}