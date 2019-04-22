using System;

namespace WeatherStationDotnet
{
    class PressureSensor : Sensor, IPressure
    {
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
