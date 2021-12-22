using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Model;

namespace FlightSimulator.ViewModels
{
    class SettingHandler:BaseNotify
    {
        ApplicationSettingsModel asm;
        private string flightServerIP;
        private string flightInfoPort;
        private string flightCommandPort;
        private Server server;
        private Client client;
        public string FlightServerIP
        {
            set
            {
                NotifyPropertyChanged("FlightServerIP");
                server.ServerIp = value;
            }
        }
        public string FlightInfoPort {
            set
            {
                NotifyPropertyChanged("FlightInfoPort");
                server.ServerPort = int.Parse(value);
            }
        }
        public string FlightCommandPort {
            set {
                NotifyPropertyChanged("FlightCommandPort");
                client.ClientPort = int.Parse(value);
            }
        }
        public SettingHandler()
        {
            server = Server.getInstance();
            client = Client.getInstance();
            asm = new ApplicationSettingsModel();
        }

        public void save()
        {
            asm.SaveSettings();
        }
        public void load()
        {
            asm.ReloadSettings();
        }
    }
}
