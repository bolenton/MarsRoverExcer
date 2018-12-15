using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MarsRover.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public int Sol { get; set; }
        public Camera Camera { get; set; }
        [JsonProperty("img_src")]
        public string ImgSrc { get; set; }

        [JsonProperty("earth_date")]
        public DateTime EarthDate { get; set; }

        public Rover Rover { get; set; }
    }

    // Just used as an easy way to wrap the list of camera since the 
    // api returns one object that wraps a list of cameras
    public class PhotoContainer
    {
        public IEnumerable<Photo> Photos { get; set; }
    }
}
