using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Model;


namespace FlightSimulator.ViewModels
{
    public class FlightBoardViewModel : BaseNotify
    {
        static FlightBoardViewModel instance = null;
        private FlightBoardViewModel() { }
        public static FlightBoardViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FlightBoardViewModel();
                }
                return instance;
            }
        }
        private double lon;
        private double lat;
        public double Lon
        {
            get { return lon; }
            set
            {
                lon = value;
                NotifyPropertyChanged("Lon");
            }
        }

        public double Lat
        {
            get { return lat; }
            set
            {
                lat = value;
                NotifyPropertyChanged("Lat");
            }
        }

        public void connect()
        {
            ConnectModel c = new ConnectModel();
            c.connect();
        }

        public void close()
        {
            Client c = Client.getInstance();
            Server s = Server.getInstance();
            c.close();
            s.close();
        }
    }
}
