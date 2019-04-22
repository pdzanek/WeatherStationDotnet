using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherStationDotnet
{
    class HumiditySensor: Sensor, IHumidity
    {
        Random rand;
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
