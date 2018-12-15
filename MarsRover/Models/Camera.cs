using Newtonsoft.Json;

namespace MarsRover.Models
{
    public class Camera
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonProperty("rover_id")]
        public int RoverId { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }
    }
}
