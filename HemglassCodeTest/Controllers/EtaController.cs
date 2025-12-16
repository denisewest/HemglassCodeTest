using HemglassCodeTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace HemglassCodeTest.Controllers
{
    [ApiController]
    [Route("api/test/route")]
    public class EtaController : Controller
    {
        private readonly RouteService _routeService;

        public EtaController(RouteService routeService) 
        { 
            _routeService = routeService;
        }

        [HttpGet]
        public async Task<IActionResult> TestRouteService(long stopId)
        {
            var routeStops = await _routeService.GetRouteById(stopId);
            if (routeStops == null)
            {
                return NotFound("No route stops found for the given stop ID.");
            }
            return Ok(routeStops);
        }
    }
}
