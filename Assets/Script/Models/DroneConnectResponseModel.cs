using System.Collections;
using System.Collections.Generic;

namespace Server.Models
{
    // see if the drone is connected, other wise set it that the drones been connected
    public class DroneConnectResponseModel
    {
        public bool connected { get; set; }
    }
}
