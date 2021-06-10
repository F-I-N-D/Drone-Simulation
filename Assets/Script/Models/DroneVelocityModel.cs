using System.Collections;
using System.Collections.Generic;

namespace Server.Models
{
    public class DroneVelocityModel
    {
        public string droneId { get; set; }
        public float velocityX { get; set; }
        public float velocityY { get; set; }
        public int rate { get; set; }
    }
}
