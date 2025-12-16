using HemglassCodeTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace HemglassCodeTest.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class EtaController : Controller
    {
        private readonly RouteService _routeService;
        private readonly OsrmService _osrmService;

        public EtaController(RouteService routeService, OsrmService osrmService) 
        { 
            _routeService = routeService;
            _osrmService = osrmService;
        }

        //[HttpGet]
        //public async Task<IActionResult> TestRouteService(long stopId)
        //{
        //    var routeStops = await _routeService.GetRouteById(stopId);

        //    if (routeStops == null)
        //    {
        //        return NotFound("No route stops found for the given stop ID.");
        //    }
        //    return Ok(routeStops);
        //}

        [HttpGet]
        public async Task<IActionResult> TestOsmrService(double fromLong, double fromLat, double toLong, double toLat)
        {
            var seconds = await _osrmService.GetExpectedTravelTimeInSeconds(fromLong, fromLat, toLong, toLat);

            if (seconds == null)
            {
                return NotFound("No duration found for the given coordinates.");
            }
            return Ok(seconds);
        }
    }
}
