using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerManagement.Application.Contracts;
using CustomerManagement.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly ICustomerFacade _customerFacade;
        private readonly ISessionFacade _sessionFacade;
        private readonly IUserFacade _userFacade;

        public CustomersController(
            ILogger<CustomersController> logger,
            ICustomerFacade customerFacade,
            ISessionFacade sessionFacade,
            IUserFacade userFacade
            )
        {
            _logger = logger;
            _customerFacade = customerFacade;
            _sessionFacade = sessionFacade;
            _userFacade = userFacade;
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
            _logger.LogInformation($"name is {name}");

            if (!sessionKey.HasValue) return Unauthorized();

            var session = _sessionFacade.GetSessionAsync(sessionKey.Value);
            if (session == null) return Unauthorized();

            var user = _userFacade.GetByEmail(session.Email);
            if (user == null) return Unauthorized();

            return Ok(await _customerFacade.Get(name, gender, city, region, classification, startDate, endDate, seller, user));
        }
    }
}