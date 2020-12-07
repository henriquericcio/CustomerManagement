using System;
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
    public class SessionsController : ControllerBase
    {
        private readonly ILogger<SessionsController> _logger;
        private readonly CustomerManagementContext _context;

        public SessionsController(ILogger<SessionsController> logger, CustomerManagementContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<dynamic>> Get(Guid id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session == null)
                return NotFound();
            
            var user = _context.Users.FirstOrDefault(u => u.Email == session.Email);
            if (user == null)
                return NotFound();

            return new { session.Id, session.Email, Role = user.Role.ToString()};
        }

        [HttpPost]
        public async Task<IActionResult> Post(Session session)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == session.Email && u.Password == session.Password);
            if(user == null)
                return Unauthorized();

            _context.Entry(session).State = EntityState.Added;
            await _context.Sessions.AddAsync(session);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = session.Id }, new { session.Id, session.Email, Role = user.Role.ToString()});
        }

 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _context.Sessions.FindAsync(id);
            if (item == null)
                return NotFound();

            _context.Sessions.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}