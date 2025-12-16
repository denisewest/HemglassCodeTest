using HemglassCodeTest.Models;

namespace HemglassCodeTest.Services
{
    public class OsrmService
    {
        private readonly HttpClient _http;

        public OsrmService(HttpClient http)
        {
            _http = http;
        }

        public async Task<double> GetExpectedTravelTimeInSeconds(double fromLong, double fromLat, double toLong, double toLat)
        {
            try 
            {
                var baseUri = $"http://router.project-osrm.org/route/v1/driving/";
                var fromCoordinates = $"{fromLong},{fromLat}%3B"; // %3B is the URL-encoded semicolon
                var toCoordinates =  $"{toLong},{toLat}?overview=false";

                var uri = new Uri(baseUri + fromCoordinates + toCoordinates); // Todo: Fix the uri so it doesn't cut off after semicolon, and works with OSRM

                var response = await _http.GetFromJsonAsync<OsrmResponse>(uri);

                if (response == null) 
                { 
                    throw new Exception("No response from OSRM service");
                }

                return response.Routes[0].Duration;
            
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while fetching travel time: {e.Message}");
                return -1;
            }
        }
    }
}
