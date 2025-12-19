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

            // Start origin at the vehicle position
            double originLong = vehicle.Longitude;
            double originLat = vehicle.Latitude;

            double cumulativeSeconds = 0;

            foreach (var stop in stops)
            {
               var travelSeconds = await _osrmService.GetExpectedTravelTimeInSeconds(originLong, originLat, stop.Longitude, stop.Latitude);

                if (travelSeconds < 0)
                {
                    // If OSRM gives error, treat as zero travel time
                    Console.WriteLine($"OSRM returned error for leg {originLong},{originLat} -> {stop.Longitude},{stop.Latitude}");
                    travelSeconds = 0;
                }

                cumulativeSeconds += travelSeconds;

                var eta = currentTime.AddSeconds(cumulativeSeconds);

                results.Add(new RouteStopEta
                {
                    StopId = stop.StopId,
                    EstimatedArrival = eta
                });

                // Move origin to this stop for the next leg
                originLong = stop.Longitude;
                originLat = stop.Latitude;
            }
            
            return results;
        }
    }
}
