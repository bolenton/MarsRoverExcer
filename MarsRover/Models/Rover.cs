using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MarsRover.Models
{
    public class Rover
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("landing_date")]
        public DateTime LandingDate { get; set; }

        [JsonProperty("launch_date")]
        public DateTime LaunchDate { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("max_sol")]
        public int MaxSol { get; set; }

        [JsonProperty("max_date")]
        public DateTime MaxDate { get; set; }

        [JsonProperty("total_photos")]
        public int TotalPhotos { get; set; }

        [JsonProperty("cameras")]
        public List<RoverCamera> Cameras { get; set; }
    }

    public class RoverContainer
    {
        public IEnumerable<Rover> Rovers { get; set; }
    }
}
