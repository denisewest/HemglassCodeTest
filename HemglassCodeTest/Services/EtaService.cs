using HemglassCodeTest.Models;

namespace HemglassCodeTest.Services
{
    public class EtaService
    {
        private readonly OsrmService _osrmService;

        public EtaService(OsrmService osrmService)
        {
            _osrmService = osrmService;
        }

        public async Task<List<RouteStopEta>> CalculateEtas(VehiclePosition vehicle, List<RouteStop> stops)
        {
            var results = new List<RouteStopEta>();
            var currentTime = DateTime.UtcNow;
            var longitude = vehicle.Longitude;
            var latitude = vehicle.Latitude;

            foreach (var stop in stops)
            {
               var travelSeconds = await _osrmService.GetExpectedTravelTimeInSeconds(longitude, latitude, stop.Longitude, stop.Latitude);

               var eta = currentTime.AddSeconds(travelSeconds);

                results.Add( new RouteStopEta
                {
                     StopId = stop.StopId,
                     EstimatedArrival = eta
                });

            }
            
            return results;
        }
    }
}
