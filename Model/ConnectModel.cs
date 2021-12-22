using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    class ConnectModel
    {
        Client client;
        Server server;
        public ConnectModel()
        {
            client = Client.getInstance();
            server = Server.getInstance();
        }
        public void connect()
        {
            server.connect_server();
            //client.connect_client();
        }
    }
}
