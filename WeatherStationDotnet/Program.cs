using System;
using System.Threading;

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

            string menuOption,opt,sensorName,sensorType,tempUnit,comparsionString;
            double value;
            Console.WriteLine("\nWciśnij dowolny klawisz, aby przejść do stacji pogodowej.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("-----------------WEATHER STATION---------------\n");
            Sensor.instances = 0;
            WeatherStation weatherStation = new WeatherStation("Weather Station no. 1");
            Thread SerializeCaller = new Thread(new ThreadStart(weatherStation.SerializeData));
            SerializeCaller.Start();
            while (true)
            {
                Console.WriteLine("1. Dodaj czujnik.");
                Console.WriteLine("2. Odczyt ze wszystkich czujników danego typu.");
                Console.WriteLine("3. Wyszukaj czujniki których odczyty spełniają określone zależności");
                Console.WriteLine("4. Wyczyść konsole\n");
                menuOption = Console.ReadLine();
                switch (menuOption)
                {
                    case "1":
                        Console.WriteLine("Wybierz typ czujnika:\n 1-Czujnik Temperatury\n 2-Czujnik Wilgoci\n 3-Czujnik ciśnienia\n 4-Czujnik temperatury i wilgoci");
                        opt = Console.ReadLine();
                        Console.WriteLine("Podaj nazwę czujnika(jeżeli puste to nazwa automatyczna)");
                        sensorName = Console.ReadLine();
                        switch (opt)
                        {
                            case "1":
                                if (sensorName.Length > 1)
                                    weatherStation.AddSensor("temperature", sensorName);
                                else
                                    weatherStation.AddSensor("temperature", "Sensor");
                                break;
                            case "2":
                                if (sensorName.Length > 1)
                                    weatherStation.AddSensor("humidity", sensorName);
                                else
                                    weatherStation.AddSensor("humidity", "Sensor");
                                break;
                            case "3":
                                if (sensorName.Length > 1)
                                    weatherStation.AddSensor("pressure", sensorName);
                                else
                                    weatherStation.AddSensor("pressure", "Sensor");
                                break;
                            case "4":
                                if (sensorName.Length > 1)
                                    weatherStation.AddSensor("tempandhumidity", sensorName);
                                else
                                    weatherStation.AddSensor("tempandhumidity", "Sensor");
                                break;
                            default:
                                Console.WriteLine("Dokonano nieprawidłowego wyboru!");
                                break;
                        }
                        Console.WriteLine(Environment.NewLine);
                        break;
                    case "2":
                        Console.WriteLine("Wybierz typ czujnika, dla którego chcesz pobrać odczyty:\nt-Temperatura\nh-Wilgoć\np-Ciśnienie");
                        sensorType = Console.ReadLine();
                        switch (sensorType.ToLower())
                        {
                            case "t":
                                Console.WriteLine("Wybierz jednostkę temperatury:\nC-Celciusz\nF-Fahrenheit\n");
                                tempUnit = Console.ReadLine();
                                if ((tempUnit.ToUpper()).Equals("F"))
                                {
                                    weatherStation.SetUnit('F');
                                }
                                else weatherStation.SetUnit('C');

                                weatherStation.GetAllDataByType('t');
                                break;
                            case "h":
                                weatherStation.GetAllDataByType('h');
                                break;
                            case "p":
                                weatherStation.GetAllDataByType('p');
                                break;
                            default:
                                Console.WriteLine("Dokonano nieprawidłowego wyboru!");
                                break;
                        }
                        Console.WriteLine(Environment.NewLine);
                        break;
                    case "3":
                        Console.WriteLine("\nWybierz typ czujnika, dla którego chcesz pobrać odczyty:\nt-Temperatura\nh-Wilgoć\np-Ciśnienie");
                        sensorType = Console.ReadLine();
                        Console.WriteLine("Wyświetl czujniki:\n" +
                            "> - wieksze niż\n" +
                            "< - mniejsze niż\n");
                        comparsionString = Console.ReadLine();
                        comparsionString.ToLower();
                        Console.WriteLine("Wartość:");
                        value = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine(sensorType + " " + comparsionString + " " + value + "\n");
                        weatherStation.GetSpecifiedData(sensorType, comparsionString, value);
                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine("-----------------WEATHER STATION---------------\n");
                        break;
                    default:
                        Console.WriteLine("Wybrano błędną opcje");
                        break;

                }
            }
        }
    }
}