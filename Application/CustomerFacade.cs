using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagement.Application.Contracts;
using CustomerManagement.Application.Contracts.Dto;
using CustomerManagement.Infrastructure;
using CustomerManagement.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.Application
{
    public class CustomerFacade : ICustomerFacade
    {
        private readonly CustomerManagementContext _context;
        private readonly ILogger<CustomerFacade> _logger;

        public CustomerFacade(ILogger<CustomerFacade> logger, CustomerManagementContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IEnumerable<CustomersDto>> Get(
            string name,
            Gender? gender,
            Guid? city,
            Guid? region,
            Classification? classification,
            DateTime? startDate,
            DateTime? endDate,
            Guid? seller,
            UserDto user
        )
        {
            var query = _context.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(c => c.Name.ToLower().StartsWith(name.ToLower()));

            if (gender.HasValue)
                query = query.Where(c => c.Gender == gender.Value);

            if (city.HasValue)
                query = query.Where(c => c.City.Id == city.Value);

            if (region.HasValue)
                query = query.Where(c => c.Region.Id == region.Value);

            if (classification.HasValue)
                query = query.Where(c => c.Classification == classification.Value);

            if (startDate.HasValue)
                query = query.Where(c => c.LastPurchase >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(c => c.LastPurchase <= endDate.Value);

            if (user.Role == Role.Seller)
                query = query.Where(c => c.Seller == user);
            else if (seller.HasValue)
                query = query.Where(c => c.Seller.Id == seller.Value);

            return await query
                .AsNoTracking()
                .Select(c => new CustomersDto
                {
                    Id = c.Id,
                    Classification = c.Classification.ToString(),
                    Name = c.Name,
                    Phone = c.Phone,
                    Gender = c.Gender.ToString(),
                    City = c.City.Name,
                    Region = c.Region.Name,
                    Seller = c.Seller.Email,
                    LastPurchase = c.LastPurchase
                })
                .ToListAsync();
        }
    }
}