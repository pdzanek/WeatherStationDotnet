using System;
using System.Runtime.Serialization;
using System.Threading;

namespace WeatherStationDotnet
{
    [DataContract]
    class HumiditySensor: Sensor, IHumidity
    {
        Random rand;
        [DataMember(Name = "Humidity")]
        double humidity;
        public HumiditySensor() : base()
        {
            rand = new Random();
            Thread MeasureCaller = new Thread(new ThreadStart(MeasureHumidity));
            MeasureCaller.Start();
        }
        public HumiditySensor(string name) : base(name)
        {
            rand = new Random();
            Thread MeasureCaller = new Thread(new ThreadStart(MeasureHumidity));
            MeasureCaller.Start();
        }
        public double Humidity
        {
            get { return humidity; }
            set
            {
                humidity = Math.Round(rand.NextDouble() * 100, 2);
                Measurement(Name + " humidity", humidity);
            }
        }
        private void MeasureHumidity()
        {
            while (true)
            {
                Humidity++;
                Thread.Sleep(5000);
            }
        }
    }
}
