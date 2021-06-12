using System;
using System.Threading.Tasks;
using CustomerManagement.Application.Contracts;
using CustomerManagement.Application.Contracts.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionsController : ControllerBase
    {
        private readonly ILogger<SessionsController> _logger;
        private readonly ISessionFacade _sessionFacade;
        private readonly IUserFacade _userFacade;

        public SessionsController(ILogger<SessionsController> logger, ISessionFacade sessionFacade, IUserFacade userFacade)
        {
            _logger = logger;
            _sessionFacade = sessionFacade;
            _userFacade = userFacade;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<dynamic>> Get(Guid id)
        {
            var session = await  _sessionFacade.GetSessionAsync(id);
            if (session == null)
                return NotFound();

            var user = _userFacade.GetByEmail(session.Email);
            if (user == null)
                return NotFound();

            return Ok(new SessionDto { Id = session.Id,Email = session.Email,Role = user.Role});
        }

        [HttpPost]
        public async Task<IActionResult> Post(SessionDto session)
        {
            var user = _userFacade.GetByEmailPassword(session.Email,session.Password);
            if(user == null)
                return Unauthorized();

            await _sessionFacade.Save(session);

            return CreatedAtAction(nameof(Get), new { id = session.Id }, new { session.Id, session.Email, Role = user.Role.ToString()});
        }

 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var session =  _sessionFacade.GetSessionAsync(id);
            if (session == null)
                return NotFound();

            await _sessionFacade.Delete(session);

            return NoContent();
        }
    }
}