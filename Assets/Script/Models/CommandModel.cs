using System.Collections;
using System.Collections.Generic;

namespace Server.Models
{
    public class CommandModel
    {
        public string command { get; set; }
        public string droneId { get; set; }
        public DroneUpdateModel data { get; set; }
    }
}
