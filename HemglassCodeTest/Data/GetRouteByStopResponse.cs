namespace HemglassCodeTest.Data
{
    public class GetRouteByStopResponse
    {
        public int StatusCode { get; set; }
        public List<RouteStopDto> Data { get; set; } = [];
        public string Message { get; set; } = "";
    }
}
