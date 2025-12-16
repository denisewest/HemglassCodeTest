using HemglassCodeTest.Models;
using HemglassCodeTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace HemglassCodeTest.Controllers
{
    [ApiController]
    [Route("api/eta")]
    public class EtaController : Controller
    {
        private readonly RouteService _routeService;
        private readonly OsrmService _osrmService; // Primarily used for testing
        private readonly EtaService _etaService;

        public EtaController(RouteService routeService, OsrmService osrmService, EtaService etaService)
        {
            _routeService = routeService;
            _osrmService = osrmService;
            _etaService = etaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEta(long stopId, double longitude, double latitude)
        {
            try
            {
                var route = await _routeService.GetRouteById(stopId);
                var vehiclePosition = new VehiclePosition
                {
                    Longitude = longitude,
                    Latitude = latitude
                };

                var etas = await _etaService.CalculateEtas(vehiclePosition, route);

                if (etas == null)
                {
                    return BadRequest("Could not calculate ETAs for the given stop ID and vehicle position.");
                }

                return Ok(etas);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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

        //[HttpGet]
        //public async Task<IActionResult> TestOsmrService(double fromLong, double fromLat, double toLong, double toLat)
        //{
        //    var seconds = await _osrmService.GetExpectedTravelTimeInSeconds(fromLong, fromLat, toLong, toLat);

        //    if (seconds == null)
        //    {
        //        return NotFound("No duration found for the given coordinates.");
        //    }
        //    return Ok(seconds);
        //}
    }
}
