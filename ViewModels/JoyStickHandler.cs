using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Model.EventArgs;
using FlightSimulator.Model;
using System.Threading;
using System.Windows.Media;

namespace FlightSimulator.ViewModels
{
    class JoyStickHandler : BaseNotify
    {
        private readonly string throttlePath = " /controls/engines/current-engine/throttle ";
        private readonly string rudderePath = " /controls/flight/rudder ";
        private readonly string aileronPath = " /controls/flight/aileron ";
        private readonly string elevatorPath = " /controls/flight/elevator ";

        static JoyStickHandler instance = null;
        private Client client = Client.getInstance();
        private JoyStickHandler() { }
        private VirtualJoystickEventArgs vj = VirtualJoystickEventArgs.getInstance();
        public static JoyStickHandler getInstance() {
            if (instance == null)
            {
                instance = new JoyStickHandler();
            }
            return instance;
        }
        public double RudderCommand
        {
            set
            {
                vj.Rudder = value;
                Thread t = new Thread(() => { client.Send(rudderePath, value); });
                t.Start();
                NotifyPropertyChanged("RudderCommand");

            }
        }
        public double ThrottleCommand {
            set {
                vj.Throttle = value;
                Thread t = new Thread(() => { client.Send(throttlePath, value); });
                t.Start();
                NotifyPropertyChanged("ThrottleCommand");

            }

        }
        public double AileronCommand {
            get
            {
                return AileronCommand;
            }
            set
            {
                vj.Aileron = value;
                 Thread t=new Thread(()=> { client.Send(aileronPath, value); });
                t.Start();
                //client.Send(aileronPath, value);
                NotifyPropertyChanged("AileronCommand");
            }
        }
        public double ElevatorCommand {
            set {
                vj.Elevator = value;
               // client.Send(elevatorPath, value);
                  Thread t = new Thread(() => { client.Send(elevatorPath, value); });
                t.Start();
                NotifyPropertyChanged("ElevatorCommand");
            }
            get
            {
                return ElevatorCommand;
            }
        }
        private string autoPilotText;
        public string AutoPilotText{
            get
            {
                return autoPilotText;
            }
            set
            {
                autoPilotText = value;
                if (!string.IsNullOrEmpty(value) && (Background == Brushes.White||Background==null))
                    Background = Brushes.LightPink; // if background is white and no text
                else if (string.IsNullOrEmpty(value)) Background = Brushes.White; // if text is not empty
                NotifyPropertyChanged("AutoPilotText");

            }
            }
        private Brush background;
        public Brush Background
        {
            get
            {
                return background;
            }
            set
            {
                background = value;
                NotifyPropertyChanged("Background");
            }
        }

        public void ok()
        {
            client.Send(autoPilotText);
            // put white background  
            Background = Brushes.White;
        }

        public void clear()
        {
            AutoPilotText = "";
            Background = Brushes.White;
        }
    }
}
