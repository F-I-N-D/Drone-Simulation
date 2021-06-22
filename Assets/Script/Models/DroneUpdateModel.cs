using System.Collections;
using System.Collections.Generic;

namespace Server.Models
{
    // all the data of the drone that's been recieved through run time can be set and get through this class
    public class DroneUpdateModel
    {
        public float batteryVoltage { get; set; }
        public bool isCharging { get; set; }
        public bool isFlying { get; set; }
        public bool isTumbled { get; set; }
        public int locationX { get; set; }
        public int locationY { get; set; }
        public int locationZ { get; set; }
        public int direction { get; set; }
        public int distanceDown { get; set; }
        public int distanceFront { get; set; }
        public int distanceBack { get; set; }
        public int distanceLeft { get; set; }
        public int distanceRight { get; set; }
        public float ldr { get; set; }
    }
}
