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
    public class SellersController : ControllerBase
    {
        private readonly ILogger<SellersController> _logger;
        private readonly CustomerManagementContext _context;

        public SellersController(ILogger<SellersController> logger, CustomerManagementContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<dynamic>>> Get()
        {
            return await _context.Users
                .Where(u=> u.Role == Role.Seller)
                .Select(u => new {
                    u.Id,
                    u.Email
                }).ToListAsync();
        }
    }
}