using System;
using System.Runtime.Serialization;
using System.Threading;

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
            Thread MeasureCaller = new Thread(new ThreadStart(MeasurePressure));
            MeasureCaller.Start();
        }
        public PressureSensor(string name) : base(name)
        {
            rand = new Random();
            Thread MeasureCaller = new Thread(new ThreadStart(MeasurePressure));
            MeasureCaller.Start();
        }

        public int Pressure
        {
            get { return pressure; }
            set
            {
                pressure = rand.Next(965, 1051);
                Measurement(Name + " pressure", pressure);
            }
        }
        private void MeasurePressure()
        {
            while (true)
            {
                Pressure++;
                Thread.Sleep(5000);
            }
        }
    }
}
