using HemglassCodeTest.Data;

namespace HemglassCodeTest.Models
{
    public class RouteStopResponse
    {
        public int StatusCode { get; set; }
        public List<RouteStopDto> Data { get; set; } = [];
        public string Message { get; set; } = "";
    }
}
