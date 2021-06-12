using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagement.Infrastructure;
using CustomerManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Application.Contracts
{
    public class SellerFacade : ISellerFacade
    {
        private readonly CustomerManagementContext _context;

        public SellerFacade(CustomerManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<dynamic>> Get()
        {
            return  _context.Users
                .Where(u=> u.Role == Role.Seller)
                .Select(u => new {
                    u.Id,
                    u.Email
                }).ToList();
        }
    }
}