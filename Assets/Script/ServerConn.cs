using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Threading;
using UnityEngine;
namespace Server
{
    public class ServerConn : MonoBehaviour
    {
        // Start is called before the first frame update
        //private HardwareDrone hd;
        public GameObject HardwareDrone;
        public int droneCount;
        public static List<GameObject> drones = new List<GameObject>();
        private void Awake()
        {
            for (int i = 0; i < droneCount; i++)
            {
                GameObject hd = Instantiate(HardwareDrone, new Vector3(i, 0, 0), transform.rotation);
                hd.GetComponent<HardwareDrone>().id = i.ToString();
                drones.Add(hd);
            }


        }
    }
}
