using System;
using System.Collections.Generic;

namespace WeatherStationDotnet
{
    class Program
    {
        static WeatherStation currentStation;
        static public List<Sensor> sensors = new List<Sensor>();
        static void Main(string[] args)
        {
            string selectStation,menuOption, opt, sensorName, sensorType, tempUnit, comparsionString;
            double value;
            bool isWeatherStationActive = false;

            Console.Clear();

            Sensor.instances = 0;
            WeatherStation weatherStation = new WeatherStation("Weather Station no.1");
            WeatherStation weatherStation2 = new WeatherStation("Weather Station no.2");
            while (true)
            {
                Console.WriteLine("Wybierz której stacji pogodowej chcesz używać:");
                Console.WriteLine("1 " + weatherStation.Name);
                Console.WriteLine("2 " + weatherStation2.Name);
                selectStation = Console.ReadLine();
                switch (selectStation)
                {
                    case "1":
                        currentStation = weatherStation;
                        isWeatherStationActive = true;
                        break;
                    case "2":
                        currentStation = weatherStation2;
                        isWeatherStationActive = true;
                        break;
                    default:
                        Console.WriteLine("Wybrano błędną opcje");
                        break;
                }
                Console.WriteLine("-----------------{0}---------------\n", currentStation.Name);
                currentStation.active = true;
                while (isWeatherStationActive)
                {
                    Console.WriteLine("1. Dodaj czujnik.");
                    Console.WriteLine("2. Odczyt ze wszystkich czujników danego typu.");
                    Console.WriteLine("3. Wyszukaj czujniki których odczyty spełniają określone zależności");
                    Console.WriteLine("4. Wyczyść konsole");
                    Console.WriteLine("5. Wypisz wszystkie dostępne czujniki");
                    Console.WriteLine("6. Powrót do głównego menu\n");
                    menuOption = Console.ReadLine();
                    switch (menuOption)
                    {
                        case "1":
                            Console.WriteLine("Wybierz typ czujnika:\n 1-Czujnik Temperatury\n 2-Czujnik Wilgoci\n 3-Czujnik ciśnienia\n 4-Czujnik temperatury i wilgoci\n");
                            opt = Console.ReadLine();
                            Console.WriteLine("Podaj nazwę czujnika(jeżeli puste to nazwa automatyczna)");
                            sensorName = Console.ReadLine();
                            switch (opt)
                            {
                                case "1":
                                    currentStation.AddSensor("temperature", sensorName);
                                    break;
                                case "2":
                                    currentStation.AddSensor("humidity", sensorName);
                                    break;
                                case "3":
                                    currentStation.AddSensor("pressure", sensorName);
                                    break;
                                case "4":
                                    currentStation.AddSensor("tempandhumidity", sensorName);
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
                                        setUnit('F');
                                    }
                                    else setUnit('C');
                                    currentStation.GetAllDataByType('t', tempUnit[0]);
                                    break;
                                case "h":
                                    currentStation.GetAllDataByType('h');
                                    break;
                                case "p":
                                    currentStation.GetAllDataByType('p');
                                    break;
                                default:
                                    Console.WriteLine("Dokonano nieprawidłowego wyboru!");
                                    break;
                            }
                            Console.WriteLine(Environment.NewLine);
                            break;
                        case "3":
                            Console.WriteLine("\nWybierz typ czujnika, dla którego chcesz pobrać odczyty:\n"+
                                                "t-Temperatura\n"+
                                                "h-Wilgoć\n"+"" +
                                                "p-Ciśnienie");
                            sensorType = Console.ReadLine();
                            Console.WriteLine("Wyświetl czujniki:\n" +
                                                "> - wieksze niż\n" +
                                                "< - mniejsze niż\n");
                            comparsionString = Console.ReadLine();
                            comparsionString.ToLower();
                            Console.WriteLine("Wartość:");
                            value = Convert.ToDouble(Console.ReadLine());
                            currentStation.GetSpecifiedData(sensorType, comparsionString, value);
                            break;
                        case "4":
                            Console.Clear();
                            break;
                        case "5":
                            foreach (Sensor sensor in sensors)
                            {
                                Console.WriteLine(sensor.Name);
                            }
                            break;
                        case "6":
                            currentStation.active = false;
                            isWeatherStationActive = false;
                            break;
                        default:
                            Console.WriteLine("Wybrano błędną opcje");
                            break;

                    }
                }
            }
            void setUnit(char unit)
            {
                foreach(Sensor sensor in sensors)
                {
                    if (sensor is ITemperature cSensor)
                    {
                        cSensor.Unit = unit;
                    }
                }
            }
        }
    }
}


/*Sensor sensor = new Sensor("name");
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


           Console.WriteLine("\nWciśnij dowolny klawisz, aby przejść do stacji pogodowej.");
           Console.ReadKey();
           */
