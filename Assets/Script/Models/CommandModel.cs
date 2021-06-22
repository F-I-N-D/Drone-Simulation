using System.Collections;
using System.Collections.Generic;

namespace Server.Models
{
    // get or set the different kind of commando's for retrieving drone data
    public class CommandModel
    {
        public string command { get; set; }
        public string droneId { get; set; }
        public DroneUpdateModel data { get; set; }
    }
}
