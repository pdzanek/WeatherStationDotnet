using System;
using System.Runtime.Serialization;

namespace WeatherStationDotnet
{
    [DataContract]
    class TemperatureSensor : Sensor, ITemperature
    {
        bool unit = true;
        [DataMember(Name ="Temperature")]
        double temperature;
        [DataMember(Name ="Unit")]
        char unitchar='C';
        Random rand;
        public TemperatureSensor() : base()
        {
            rand = new Random();
        }

        public TemperatureSensor(string name) : base(name)
        {
            rand = new Random();
        }
        public double Temperature
        {
            get { return temperature; }
            set
            {
                if (unit)
                {
                    if (rand.Next(1) == 0)
                        temperature = rand.Next(55);
                    else
                        temperature = -rand.Next(55);
                }
                else
                {
                    if (rand.Next(1) == 0)
                        temperature = Math.Round(rand.Next(55) * 1.8 + 32, 2);
                    else
                        temperature = Math.Round(-rand.Next(55) * 1.8 + 32, 2);
                }
                Measurement(Name+" temperature", temperature);
            }
        }

        public char Unit
        {
            get { return unitchar; }
            set { unitchar = value;
                if (unitchar.Equals('C')) unit = true;
                else unit = false;
            }
        }
    }
}
