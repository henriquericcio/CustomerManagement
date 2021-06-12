using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagement.Application;
using CustomerManagement.Application.Contracts;
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
        private readonly ISellerFacade _sellerFacade;

        public SellersController(ILogger<SellersController> logger, ISellerFacade sellerFacade)
        {
            _logger = logger;
            _sellerFacade = sellerFacade;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<dynamic>>> Get()
        {
            return Ok( await _sellerFacade.Get());
        }
    }
}