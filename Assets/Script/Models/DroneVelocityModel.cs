using System.Collections;
using System.Collections.Generic;

namespace Server.Models
{
    // getter and setters for the velocity data
    public class DroneVelocityModel
    {
        public string droneId { get; set; }
        public float velocityX { get; set; }
        public float velocityY { get; set; }
        public int rate { get; set; }
    }
}
