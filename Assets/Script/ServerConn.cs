
using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Threading;
using UnityEngine;
using System;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading;

using Server.Models;
namespace Server
{
   
    public class ServerConn : MonoBehaviour
    {
        private int port = 8000;
        private string server = "145.137.51.223";
        Socket socket;
        public GameObject hardwareDrone;
        public GameObject digitalDrone;
        public int borderx = 300;
        public int bordery = 200;
        
        public int droneCount;
        NetworkStream stream;
        public static List<GameObject> hardwareDrones = new List<GameObject>();
        public static List<GameObject> softwareDrones = new List<GameObject>();

        // during start a socket is made and is connected to the server
        // software drones are created and loaded in the simulation
        // the hardware drones are loaded in the simulation
        // all the software drones are connected with the server. This loop will go on until all software drones are connected
        void Start()
        {
            socket = new Socket(server, port);
            socket.Connect();
            CreateSoftwareDrone();
            CreateHardwareDrone();
            foreach (var drone in softwareDrones)
            {
                while (!socket.ConnectSoftwareDrone(drone.GetComponent<DigitalDrone>().id))
                {
                    Thread.Sleep(500);
                }
            }
        }

        // Every frame the software drone and hardware drone will move
        void Update(){
            MoveSoftwareDrone();
            MoveHardwareDrone();
        }

        // method for setting the hardwareDrones in the simulation on the same position as in the real world
        // the drones have the same led combination as the real hardware drones
        public void CreateHardwareDrone()
        {
            foreach (var drone in socket.GetHardwareDrones())
            {
                GameObject hd = Instantiate(hardwareDrone, new Vector3(drone.locationX/100.0f, drone.locationZ/50.0f, 10.8f - drone.locationY/100.0f), transform.rotation);
                hd.GetComponent<HardwareDrone>().id = drone.droneId;
                hd.GetComponentInChildren<FrontLed>().colorFront = drone.colorFront;
                hd.GetComponentInChildren<BackLed>().colorBack = drone.colorBack;
                hardwareDrones.Add(hd);
            }
        }

        //move the drones with a set velocity that's been given by the server
        public void MoveHardwareDrone()
        {
            foreach (var drone in socket.GetHardwareDrones())
            {
                GameObject hwDrone = hardwareDrones.Find(hardwareDrone => hardwareDrone.GetComponent<HardwareDrone>().id == drone.droneId);
                hwDrone.transform.position = new Vector3(drone.locationX / 100.0f, drone.locationZ/50.0f, 10.8f - drone.locationY / 100.0f);
                hwDrone.transform.rotation = new Quaternion(0,(float)drone.direction,0,0);
            }
        }

        // method for setting the softwareDrones in the simulation on random position
        // the drones have the led combination that are given by the server
        
        public void CreateSoftwareDrone()
        {
            System.Random rand = new System.Random();
            foreach(var drone in socket.GetSoftwareDrones())
            {
                float randomX = rand.Next(borderx, 1919-borderx)/100.0f;
                float randomZ = rand.Next(bordery, 1079-bordery)/100.0f;
                GameObject di = (GameObject)Instantiate(digitalDrone, new Vector3(randomX, drone.locationY, randomZ ), transform.rotation);
                di.GetComponent<DigitalDrone>().id = drone.droneId;
                di.GetComponentInChildren<FrontLed>().colorFront = drone.colorFront;
                di.GetComponentInChildren<BackLed>().colorBack = drone.colorBack;
                softwareDrones.Add(di);
            }
        }

        public void SetSoftwareDrone(GameObject drone)
        {
            var newData = new DroneUpdateModel
            {
                locationX = (int)(drone.GetComponent<DigitalDrone>().transform.position.x * 100.0f),
                locationY = (int)((10.8 - drone.GetComponent<DigitalDrone>().transform.position.z) * 100.0f),
                locationZ = (int)(drone.GetComponent<DigitalDrone>().transform.position.y * 50.0f),
                ldr = drone.GetComponentInChildren<LDR>().lightLevel,
                isFlying = true
            };
            socket.SetSoftwareDrone(drone.GetComponent<DigitalDrone>().id, newData);
        }

        // using the id the drones get the right velocity and also gets the right position
        public void MoveSoftwareDrone()
        {
            foreach (var drone in socket.GetSoftwareDrones())
            {
                GameObject swDrone = softwareDrones.Find(digitalDrone => digitalDrone.GetComponent<DigitalDrone>().id == drone.droneId);
                swDrone.GetComponent<DigitalDrone>().transform.position = new Vector3(swDrone.GetComponent<DigitalDrone>().transform.position.x, drone.locationZ/50.0f, swDrone.GetComponent<DigitalDrone>().transform.position.z);
            }
                foreach (var drone in softwareDrones)
            {
                var velDrone = socket.GetSoftwareDroneVelocity(drone.GetComponent<DigitalDrone>().id);
                drone.transform.Translate(-drone.GetComponent<DigitalDrone>().speed * velDrone.velocityX,0, -drone.GetComponent<DigitalDrone>().speed * velDrone.velocityY);
                SetSoftwareDrone(drone);
            }
        }
       
    }
}
