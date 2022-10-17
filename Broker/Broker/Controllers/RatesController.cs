using Contracts.Engine;
using Microsoft.AspNetCore.Mvc;
using Models.Response;
using System;
using System.Threading.Tasks;

namespace Broker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatesController : ControllerBase
    {
        private readonly IBrokerEngine _engine;

        public RatesController(IBrokerEngine engine)
        {
            _engine = engine;
        }

        [HttpGet("best")]

        public async Task<IActionResult> best(DateTime startDate, DateTime DateTime, decimal moneyUsd)
        {
            try
            {
                var resp = await _engine.GetBestRate(startDate, DateTime, moneyUsd);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
