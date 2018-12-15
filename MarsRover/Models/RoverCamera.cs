using Newtonsoft.Json;

namespace MarsRover.Models
{
    public class RoverCamera
    {
        public string Name { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }
    }
}
