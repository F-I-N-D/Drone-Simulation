
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
using DroneInfo;
using TinyJson;

namespace Server
{
   
    public class ServerConn : MonoBehaviour
    {
        DroneData droneData;
        [ThreadStatic] static Stack<List<string>> splitArrayPool;
        [ThreadStatic] static StringBuilder stringBuilder;
        [ThreadStatic] static Dictionary<Type, Dictionary<string, FieldInfo>> fieldInfoCache;
        [ThreadStatic] static Dictionary<Type, Dictionary<string, PropertyInfo>> propertyInfoCache;
        // Start is calle
        string server = "145.137.51.223";
        int portnumber = 8000;
        
        string message = "{\"command\":\"getHardwareDrones\" }";
        // before the first frame update
        //private HardwareDrone hd;
        public GameObject HardwareDrone;
        public int droneCount;
        NetworkStream stream;
        public static List<GameObject> drones = new List<GameObject>();
        void Awake()
        {
            //for (int i = 0; i < droneCount; i++)
            //{
            //    GameObject hd = Instantiate(HardwareDrone, new Vector3(i, 0, 0), transform.rotation);
            //    hd.GetComponent<HardwareDrone>().id = i.ToString();
            //    drones.Add(hd);
            //}


        }

        void Start()
        {
            Connection(server,portnumber);
            Debug.Log("connected");
            SendMessage(message);
            Thread.Sleep(1000);
            SetHardwareDrone();
        }
        void Update(){
           
                //Thread.Sleep(1000);
                //RecieveMessage();
                MoveHardwareDrone();
            
        }

        private void ClientSocket(string server_, int portnumber_, string message_){
           
                //stream.Close();
                //client.Close(); 
            
        }
         public void Connection(string server_, int portnumber_)
        {
            try
            {

                TcpClient client = new TcpClient(server_, portnumber_);

                stream = client.GetStream();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }

        public void SendMessage(string message_)
        {
            try
            {
                Byte[] data = Encoding.ASCII.GetBytes(message_);
                // Buffer to store the response bytes.
                stream.Write(data, 0, data.Length);
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
        }

        public string RecieveMessage()
        {
            try
            {
                
                Byte[] data = new Byte[4096];
                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                
               // Debug.Log(responseData);
                
                return responseData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
                return null;
            }
        }

        public void SetHardwareDrone()
        {

            //Debug.Log(JSONParser.FromJson<List<DroneData>>(RecieveMessage()).ToString());
            foreach(DroneData drone in JSONParser.FromJson<List<DroneData>>(RecieveMessage()))
            {
                Debug.Log(drone.droneId);
                 GameObject hd = Instantiate(HardwareDrone, new Vector3(drone.locationX, drone.locationY, drone.locationZ), transform.rotation);
                hd.GetComponent<HardwareDrone>().id = drone.droneId;
                drones.Add(hd);
            }



        }

        public void MoveHardwareDrone()
        {
            SendMessage(message);
            Debug.Log(JSONParser.FromJson<List<DroneData>>(RecieveMessage()).ToString());
            foreach (DroneData drone in JSONParser.FromJson<List<DroneData>>(RecieveMessage()))
            {
                GameObject iets = drones.Find(hardwareDrone => hardwareDrone.GetComponent<HardwareDrone>().id == drone.droneId);
                iets.transform.position += new Vector3(drone.locationX, drone.locationY, drone.locationZ);

            }
        }

        public void SetSoftwareDrone()
        {

        }
       
    }
}
