using System;
using System.Runtime.Serialization;

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
        }
        public HumiditySensor(string name) : base(name)
        {
            rand = new Random();
        }
        public double Humidity
        {
            get { return humidity; }
            set { humidity = Math.Round(rand.NextDouble() * 100, 2); }
        }
    }
}
