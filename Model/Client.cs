using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using FlightSimulator.Model.EventArgs;

namespace FlightSimulator.Model
{
    class Client
    {
        private readonly string throttlePath = " /controls/engines/current-engine/throttle ";
        private readonly string rudderePath = " /controls/flight/rudder ";
        private readonly string aileronPath = " /controls/flight/aileron ";
        private readonly string elevatorPath = " /controls/flight/elevator ";

        private bool stop = false;
        VirtualJoystickEventArgs vj = VirtualJoystickEventArgs.getInstance();
        static Client instance = null;
        private int clientPort = FlightSimulator.Properties.Settings.Default.FlightCommandPort;
        TcpClient client;
        public int ClientPort
        {
            get { return clientPort; }
            set { clientPort = value; }
        }
        private Client() { }
        public static Client getInstance()
        {
            if (instance == null)
            {
                instance = new Client();
                return instance;
            }
            else return instance;
        }
        public void connect_client()
        {
            while (true)
            {
                //  IPEndPoint ep = new IPEndPoint(IPAddress.Parse("10.0.0.2"), 55555);
                client = new TcpClient();
                try
                {
                    client.Connect(IPAddress.Parse("127.0.0.1"), clientPort);
                    break;
                }
                catch
                {
                    continue;
                }
            }
            Console.WriteLine("You are connected");
            // client.Close();

        }

        //for auto pilot
        public void Send(string input)
        {


            NetworkStream stream = client.GetStream();
            BinaryWriter writer = new BinaryWriter(stream);
            {
                if (string.IsNullOrEmpty(input)) return; // in case where user pressed ok and text is empty
                string[] commands = input.Split(' ');
                string path = "none";
                int i;
                for (i = 0; i < commands.Length; ++i)
                {
                    if (commands[i].Equals("throttle"))
                    {
                        path = throttlePath;
                        break;
                    }
                    else if (commands[i].Equals("rudder"))
                    {
                        path = rudderePath;
                        break;
                    }
                    else if (commands[i].Equals("aileron"))
                    {
                        path = aileronPath;
                        break;
                    }
                    else if (commands[i].Equals("elevator"))
                    {
                        path = elevatorPath;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                if (path.Equals("none")) return;
                string[] num = commands[++i].Split('\n');
                double val = Double.Parse(num[0]);
                //Console.WriteLine("Command is: " + command);
                //byte[] data = Encoding.ASCII.GetBytes(command + "\r\n");
                //Console.WriteLine("Data is: " + data.ToString());
                //stream.Write(data, 0, data.Length);

                string tmp = "set " + path + val + "\r\n";
                writer.Write(Encoding.ASCII.GetBytes(tmp));

                //Thread.Sleep(2000); // 2 seconds delay

            }
        }
        //for joystick
        public void Send(string input, double value)
        {
            if (client == null) return;
            NetworkStream stream = client.GetStream();
            BinaryWriter writer = new BinaryWriter(stream);
            {
                if (string.IsNullOrEmpty(input)) return; // in case where user pressed ok and text is empty

                //Console.WriteLine("Command is: " + command);
                //byte[] data = Encoding.ASCII.GetBytes(command + "\r\n");
                //Console.WriteLine("Data is: " + data.ToString());
                //stream.Write(data, 0, data.Length);

                string tmp = "set " + input + value.ToString() + "\r\n";
                writer.Write(Encoding.ASCII.GetBytes(tmp));

                //Thread.Sleep(2000); // 2 seconds delay
            }
        }
public void close()
        {
            if(client!=null)
               client.Close();
        }
    }
}