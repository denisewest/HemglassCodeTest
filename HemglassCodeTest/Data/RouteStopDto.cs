using System.Runtime.CompilerServices;

namespace HemglassCodeTest.Data
{
    public class RouteStopDto
    {
        public long StopId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string NextDate { get; set; } = string.Empty;
        public string NextTime { get; set; } = string.Empty;
        public string StreetAddress { get; set; } = string.Empty;
        public string StreetNumber { get; set; } = string.Empty;
    }
}
