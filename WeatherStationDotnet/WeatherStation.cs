using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherStationDotnet
{
    class WeatherStation
    {
        static List<Sensor> sensors;
        string Name;
        static bool unit;
        public WeatherStation(string WeatherStationName)
        {
            Name = WeatherStationName;
            sensors = new List<Sensor>();
        }

        void AddSensor(string name)
        {
            switch (name.ToLower())
            {
                case "temperature":
                    sensors.Add(new TemperatureSensor());
                    Console.WriteLine("Dodano czujnik!");
                    break;
                case "humidity":
                    sensors.Add(new HumiditySensor());
                    Console.WriteLine("Dodano czujnik!");
                    break;
                case "pressure":
                    sensors.Add(new PressureSensor());
                    Console.WriteLine("Dodano czujnik!");
                    break;
                case "tempandhumidity":
                    sensors.Add(new TemperatureAndHumiditySensor());
                    break;
                default:
                    Console.WriteLine("Podano nieprawidłowy typ czujnika.");
                    break;
            }
        }
    }
}
