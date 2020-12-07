using System.Collections.Generic;
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
    public class RegionsController : ControllerBase
    {
        private readonly ILogger<RegionsController> _logger;
        private readonly CustomerManagementContext _context;

        public RegionsController(ILogger<RegionsController> logger, CustomerManagementContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Region>>> Get()
        {
            return await _context.Regions.ToListAsync();
        }
    }
}