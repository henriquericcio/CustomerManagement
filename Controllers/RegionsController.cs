using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerManagement.Application.Contracts;
using CustomerManagement.Application.Contracts.Dto;
using CustomerManagement.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : ControllerBase
    {
        private readonly ILogger<RegionsController> _logger;
        private readonly IRegionFacade _regionFacade;

        public RegionsController(
            ILogger<RegionsController> logger,
            IRegionFacade regionFacade
            )
        {
            _logger = logger;
            _regionFacade = regionFacade;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegionDto>>> Get()
        {
            return Ok(await _regionFacade.Get());
        }
    }
}