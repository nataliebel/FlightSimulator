using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using FlightSimulator.Model.EventArgs;
using FlightSimulator.ViewModels;
using System.Threading;

namespace FlightSimulator.Model
{
    class Server
    {
        FlightBoardViewModel fbv = FlightBoardViewModel.Instance;
        TcpListener listener;
        static Server instance = null;
        private string serverIp = FlightSimulator.Properties.Settings.Default.FlightServerIP;
        private int serverPort = FlightSimulator.Properties.Settings.Default.FlightInfoPort;
        public int ServerPort
        {
            get { return serverPort; }
            set
            {
                serverPort = value;
            }
        }
        public string ServerIp
        {
            get { return serverIp; }
            set { serverIp = value; }
        }
        private Server()
        {
        }
        static public Server getInstance()
        {
            if (instance == null)
            {
                instance = new Server();
                return instance;
            }
            else return instance;
        }

        public void connect_server()
        {
            //---listen at the specified IP and port no.---
            IPAddress localAdd = IPAddress.Parse(serverIp);
            listener = new TcpListener(localAdd, serverPort);
            Console.WriteLine("Listening...");

            // Task t = new Task(new Action(open));
            //t.Start();
            Thread t = new Thread(() => { open(); });
            t.Start();
        }

        public void open()
        {
            listener.Start();
            //---incoming client connected---
            TcpClient client = listener.AcceptTcpClient();
            while (true)
            {


                //---get the incoming data through a network stream---
                NetworkStream nwStream = client.GetStream();
                byte[] buffer = new byte[client.ReceiveBufferSize];

                //---read incoming stream---
                int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

                //split the string by ,
                string[] words = System.Text.Encoding.Default.GetString(buffer).Split(',');
                fbv.Lon = Double.Parse(words[0]);
                fbv.Lat = Double.Parse(words[1]);

                //---convert the data received into a string---
                string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Received : " + dataReceived);

                //---write back the text to the client---
                Console.WriteLine("Sending back : " + dataReceived);
                // nwStream.Write(buffer, 0, bytesRead);
                //client.Close();
            }
        }

        public void close()
        {
            if (listener!=null)
                listener.Stop();
        }
    }
}