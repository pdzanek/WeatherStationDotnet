using System;

namespace WeatherStationDotnet
{
    class Program
    {
        static void Main(string[] args)
        {
            Sensor sensor = new Sensor("name");
            Sensor sensor1 = new Sensor("name56789qwertyzxcvbnm");
            Console.WriteLine(sensor.Name);
            Console.WriteLine(sensor1.Name);
            PressureSensor pressureSensor1 = new PressureSensor("PressureSensor");
            pressureSensor1.Pressure++;
            Console.WriteLine("{0} hPa", pressureSensor1.Pressure);
            TemperatureSensor temperatureSensor1 = new TemperatureSensor("TemperatureSensor");
            temperatureSensor1.Temperature++;
            Console.WriteLine(temperatureSensor1.Name);
            Console.WriteLine("{0} {1}",temperatureSensor1.Temperature, temperatureSensor1.Unit);
            TemperatureAndHumiditySensor tah1 = new TemperatureAndHumiditySensor("TemperatureAndHumiditySensor");
            tah1.Humidity++;
            tah1.Temperature++;
            Console.WriteLine("{0} {1}", tah1.Temperature, tah1.Unit);
            Console.WriteLine("{0}%",tah1.Humidity);
            Console.ReadKey();
        }
    }
}
/* if (pressureSensor1 is IPressure)
            {
                IPressure ip = pressureSensor1 as IPressure;
                Console.WriteLine(ip.Pressure);
            }
*/