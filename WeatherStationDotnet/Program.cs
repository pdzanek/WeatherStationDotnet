using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Console.WriteLine(pressureSensor1.Pressure + " hPa");
            TemperatureSensor temperatureSensor1 = new TemperatureSensor("TemperatureSensor");
            temperatureSensor1.Temperature++;
            Console.WriteLine(temperatureSensor1.Name);
            Console.WriteLine(temperatureSensor1.Temperature +" "+ temperatureSensor1.Unit);
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