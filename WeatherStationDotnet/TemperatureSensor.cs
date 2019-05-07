using System;
using System.Runtime.Serialization;
using System.Threading;

namespace WeatherStationDotnet
{
    class TemperatureSensor : Sensor, ITemperature
    {
        bool unit = true;
        double temperature;
        char unitchar='C';
        Random rand;
        public TemperatureSensor() : base()
        {
            rand = new Random();
            Thread MeasureCaller = new Thread(new ThreadStart(MeasureTemperature));
            MeasureCaller.Start();
        }

        public TemperatureSensor(string name) : base(name)
        {
            rand = new Random();
            Thread MeasureCaller = new Thread(new ThreadStart(MeasureTemperature));
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
                Measurement(Name+" temperature "+Unit, temperature);
            }
        }
        
        private void MeasureTemperature()
        {
            while (true)
            {
                Temperature++;
                Thread.Sleep(5000);
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
