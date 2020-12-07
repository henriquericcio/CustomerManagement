using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagement.Infrastructure;
using CustomerManagement.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly CustomerManagementContext _context;

        public CustomersController(ILogger<CustomersController> logger, CustomerManagementContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<dynamic>>> Get(
            string name,
            Gender? gender,
            Guid? city,
            Guid? region,
            Classification? classification,
            DateTime? startDate,
            DateTime? endDate,
            Guid? seller,
            [FromHeader] Guid? sessionKey
        )
        {
            if (!sessionKey.HasValue)
                return Unauthorized();

            var session = _context.Sessions.FirstOrDefault(s => s.Id == sessionKey.Value);
            if (session == null)
                return Unauthorized();

            var user = _context.Users.FirstOrDefault(u => u.Email == session.Email);
            if (user == null)
                return Unauthorized();

            _logger.LogInformation($"name is {name}");

            var query = _context.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(c => c.Name.ToLower().StartsWith(name.ToLower()));

            if (gender.HasValue)
                query = query.Where(c => c.Gender == gender.Value);

            if (city.HasValue)
            {
                var cityFilter = await _context.Cities.FindAsync(city);
                if (cityFilter == null)
                    return BadRequest("city not exists");

                query = query.Where(c => c.City == cityFilter);
            }

            if (region.HasValue)
            {
                var regionFilter = await _context.Regions.FindAsync(region);
                if (regionFilter == null)
                    return BadRequest();

                query = query.Where(c => c.Region == regionFilter);
            }

            if (classification.HasValue)
                query = query.Where(c => c.Classification == classification.Value);

            if (startDate.HasValue)
                query = query.Where(c => c.LastPurchase >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(c => c.LastPurchase <= endDate.Value);

            if (user.Role == Role.Seller)
                query = query.Where(c => c.Seller == user);
            else if(seller.HasValue)
                query = query.Where(c => c.Seller.Id == seller.Value);

            return await query.Select(c => new
            {
                c.Id,
                Classification = c.Classification.ToString(),
                c.Name,
                c.Phone,
                Gender = c.Gender.ToString(),
                City = c.City.Name,
                Region = c.Region.Name,
                Seller = c.Seller.Email,
                c.LastPurchase
            }).ToListAsync();
        }
    }
}