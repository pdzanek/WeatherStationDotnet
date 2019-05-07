using System;
using System.Runtime.Serialization;
using System.Threading;

namespace WeatherStationDotnet
{
    class TemperatureAndHumiditySensor : Sensor, ITemperature, IHumidity
    {
        [DataMember(Name = "Humidity")]
        public double humidity;
        bool unit = true;
        [DataMember(Name = "Temperature")]
        double temperature;
        [DataMember(Name = "Unit")]
        char unitchar = 'C';
        Random rand;
        public TemperatureAndHumiditySensor() : base()
        {
            rand = new Random();
            Thread MeasureCaller = new Thread(new ThreadStart(MeasureTemperatureAndHumidity));
            MeasureCaller.Start();
        }

        public TemperatureAndHumiditySensor(string name) : base(name)
        {
            rand = new Random();
            Thread MeasureCaller = new Thread(new ThreadStart(MeasureTemperatureAndHumidity));
            MeasureCaller.Start();
        }

        public double Temperature
        {
            get { return temperature; }
            set
            {
                if (unit)
                {
                    if (rand.Next(2) == 0)
                        temperature = rand.Next(55);
                    else
                        temperature = -rand.Next(55);
                }
                else
                {
                    if (rand.Next(2) == 0)
                        temperature = Math.Round(rand.Next(55) * 1.8 + 32, 2);
                    else
                        temperature = Math.Round(-rand.Next(55) * 1.8 + 32, 2);
                }
                Measurement(Name + " temperature " + Unit, temperature);
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
            set
            {
                humidity = Math.Round(rand.NextDouble() * 100,2);
                Measurement(Name + " humidity", humidity);
            }
        }
        private void MeasureTemperatureAndHumidity()
        {
            while (true)
            {
                Humidity++;
                Temperature++;
                Thread.Sleep(5000);
            }
        }
    }
}
