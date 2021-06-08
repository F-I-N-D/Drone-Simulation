using System;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using TinyJson;
using Server.Models;

namespace Server
{
    public class Socket
    {
        public static string Server { get; }
        public static int Port { get; }
        private static NetworkStream Stream;
        private static TcpClient Client;

        public Socket(string server, int port)
        {
            Server = server
            Port = port
        }

        public bool Connect()
        {
            try
            {
                Client = new TcpClient(Server, Port);
                Stream = Client.GetStream();
                return true;
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
                return false;
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
                return false;
            }
        }

        public List<DroneDataModel> GetSoftwareDrones()
        {
            command = new CommandModel{
                command = "getSoftwareDrones"
            };
            
            SendMessage(command);
            string message = RecieveMessage();
            return JSONParser.FromJson<List<DroneData>>(message);
        }

        public List<DroneDataModel> GetHardwareDrones()
        {
            command = new CommandModel{
                command = "getHardwareDrones"
            };
            
            SendMessage(command);
            string message = RecieveMessage();
            return JSONParser.FromJson<List<DroneData>>(message);
        }

        public List<DroneVelocityModel> GetSoftwareDroneVelocity(string droneId)
        {
            command = new CommandModel{
                command = "getSoftwareDroneVelocity",
                droneId
            };
            
            SendMessage(command);
            string message = RecieveMessage();
            return JSONParser.FromJson<List<DroneVelocityModel>>(message);
        }

        public bool ConnectSoftwareDrone(string droneId)
        {
            command = new CommandModel{
                command = "connectSoftwareDrone",
                droneId
            };
            
            SendMessage(command);
            string message = RecieveMessage();
            return JSONParser.FromJson<DroneConnectResponseModel>(message).connected;
        }

        public bool SetSoftwareDrone(string droneId, DroneUpdateModel updateData)
        {
            command = new CommandModel{
                command = "setSoftwareDrone",
                droneId,
                data = updateData
            };
            
            SendMessage(command);
            string message = RecieveMessage();
            return JSONParser.FromJson<DroneUpdateResponseModel>(message).set;
        }

        private bool SendMessage(CommandModel command)
        {
            try
            {
                string message = JSONWriter.ToJson(command);
                Byte[] data = Encoding.ASCII.GetBytes(message);
                stream.Write(data, 0, data.Length);
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
                return false;
            }
        }

        public string RecieveMessage()
        {
            try
            {
                Byte[] data = new Byte[4096];
                String responseData = String.Empty;
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);         
                return responseData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
                return null;
            }
        }

        public void Disconnect()
        {
            Stream.Close();
            Cleint.Close();
        }
    }
}