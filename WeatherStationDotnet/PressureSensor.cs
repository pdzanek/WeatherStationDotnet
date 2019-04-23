using System;
using System.Runtime.Serialization;

namespace WeatherStationDotnet
{
    [DataContract]
    class PressureSensor : Sensor, IPressure
    {
        [DataMember(Name = "Pressure")]
        int pressure;
        Random rand;
        public PressureSensor() : base()
        {
            rand = new Random();
        }
        public PressureSensor(string name) : base(name)
        {
            rand = new Random();
        }

        public int Pressure
        {
            get { return pressure; }
            set => pressure =  rand.Next(965, 1051);
        }
    }
}
