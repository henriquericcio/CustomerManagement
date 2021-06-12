using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagement.Application.Contracts;
using CustomerManagement.Application.Contracts.Dto;
using CustomerManagement.Infrastructure;
using CustomerManagement.Model;

namespace CustomerManagement.Application
{
    public class SellerFacade : ISellerFacade
    {
        private readonly CustomerManagementContext _context;

        public SellerFacade(CustomerManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SellerDto>> Get()
        {
            return  _context.Users
                .Where(u=> u.Role == Role.Seller)
                .Select(u => new SellerDto{
                    Id = u.Id,
                    Email = u.Email
                }).ToList();
        }
    }
}