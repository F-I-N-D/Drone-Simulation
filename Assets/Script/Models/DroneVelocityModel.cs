using System.Collections;
using System.Collections.Generic;

namespace Server.Models
{
    public class DroneVelocityModel
    {
        public string droneId { get; set; }
        public int velocityX { get; set; }
        public int velocityY { get; set; }
        public int rate { get; set; }
    }
}
