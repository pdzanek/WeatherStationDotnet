using System;

namespace WeatherStationDotnet
{
    class TemperatureAndHumiditySensor : Sensor, ITemperature, IHumidity
    {
        public double humidity;
        bool unit = true;
        double temperature;
        char unitchar = 'C';
        Random rand;
        public TemperatureAndHumiditySensor() : base()
        {
            rand = new Random();
        }

        public TemperatureAndHumiditySensor(string name) : base(name)
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
                    {
                        temperature = rand.Next(55);
                    }
                    else
                    {
                        temperature = -rand.Next(55);
                    }
                }
                else
                {
                    if (rand.Next(1) == 0)
                    {
                        temperature = Math.Round(rand.Next(55) * 1.8 + 32, 2);

                    }
                    else
                    {
                        temperature = Math.Round(-rand.Next(55) * 1.8 + 32, 2);
                    }
                }
            }
        }
        public char Unit
        {
            get { return unitchar; }
            set
            {
                unitchar = value;
                if (unitchar.Equals('C')) unit = true;
                else unit = false;
            }
        }

        public double Humidity
        {
            get { return humidity; }
            set { humidity = Math.Round(rand.NextDouble() * 100, 2); }
        }
    }
}
