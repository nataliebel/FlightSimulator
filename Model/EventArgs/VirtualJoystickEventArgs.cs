using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model.EventArgs
{
    public class VirtualJoystickEventArgs
    {
        static VirtualJoystickEventArgs instance = null;
    public double Aileron { get; set; }
        public double Elevator { get; set; }
        public double Throttle {
            set { Console.WriteLine("xoxo"); } }
        public double Rudder { get; set; }
        private VirtualJoystickEventArgs(){}
        static public  VirtualJoystickEventArgs getInstance()
        {
            if (instance == null)
            {
                instance = new VirtualJoystickEventArgs();
                return instance;
            }
            else return instance;
        }
}
}
