using System;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using TinyJson;
using Server.Models;
using System.Text;
namespace Server
{
    public class Socket
    {
        private string Server;
        private int Port;
        private static NetworkStream Stream;
        private static TcpClient Client;

        public Socket(string server, int port)
        {
            Server = server;
            Port = port;
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
            var command = new CommandModel
            {
                command = "getSoftwareDrones"
            };
            
            SendMessage(command);
            string message = RecieveMessage();
            return JSONParser.FromJson<List<DroneDataModel>>(message);
        }

        public List<DroneDataModel> GetHardwareDrones()
        {
            var command = new CommandModel{
                command = "getHardwareDrones"
            };
            
            SendMessage(command);
            string message = RecieveMessage();
            return JSONParser.FromJson<List<DroneDataModel>>(message);
        }

        public DroneVelocityModel GetSoftwareDroneVelocity(string droneId)
        {
            var command = new CommandModel {
                command = "getSoftwareDroneVelocity",
               droneId  = droneId
            };
            
            SendMessage(command);
            string message = RecieveMessage();
            return JSONParser.FromJson<DroneVelocityModel>(message);
        }

        public bool ConnectSoftwareDrone(string droneId)
        {
            var command = new CommandModel{
                command = "connectSoftwareDrone",
                droneId =droneId
            };
            
            SendMessage(command);
            string message = RecieveMessage();
            return JSONParser.FromJson<DroneConnectResponseModel>(message).connected;
        }

        public bool SetSoftwareDrone(string droneId, DroneUpdateModel updateData)
        {
            var command = new CommandModel{
                command = "setSoftwareDrone",
                droneId = droneId,
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
                Stream.Write(data, 0, data.Length);
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
                Int32 bytes = Stream.Read(data, 0, data.Length);
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
            Client.Close();
        }
    }
}