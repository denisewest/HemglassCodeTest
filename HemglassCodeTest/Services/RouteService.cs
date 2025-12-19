using HemglassCodeTest.Data;
using HemglassCodeTest.Models;
using System.Runtime.CompilerServices;

namespace HemglassCodeTest.Services
{
    public class RouteService
    {
        private readonly HttpClient _http;

        public RouteService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<RouteStop>> GetRouteById(long stopId)
        {
            try 
            { 
                var url = $"https://iceman-prod.azurewebsites.net/api/tracker/getroutebystop?stopId={stopId}";
                var response = await _http.GetFromJsonAsync<RouteStopResponse>(url);
            
                if (response == null || response.StatusCode != 200)
                {
                    throw new Exception("No response from Iceman service");
                }

                 return response.Data.Select(r => new RouteStop 
                    {
                        StopId = r.StopId,
                        Longitude = r.Longitude,
                        Latitude = r.Latitude,
                        PlannedArrival = DateTime.Parse(r.NextDate)
                    })
                    .OrderBy(r => r.PlannedArrival)
                    .ToList();

            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
                return [];
            }
        }
    }
}
