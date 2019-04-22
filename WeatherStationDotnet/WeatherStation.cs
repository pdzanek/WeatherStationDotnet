﻿using System;
using System.Collections.Generic;

namespace WeatherStationDotnet
{
    class WeatherStation
    {
        static List<Sensor> sensors;
        string Name;
        static char unit='C';
        public WeatherStation(string WeatherStationName)
        {
            Name = WeatherStationName;
            sensors = new List<Sensor>();
        }

        void AddSensor(string name)
        {
            bool err = false;
            switch (name.ToLower())
            {
                case "temperature":
                    sensors.Add(new TemperatureSensor());
                    break;
                case "humidity":
                    sensors.Add(new HumiditySensor());
                    break;
                case "pressure":
                    sensors.Add(new PressureSensor());
                    break;
                case "tempandhumidity":
                    sensors.Add(new TemperatureAndHumiditySensor());
                    break;
                default:
                    Console.WriteLine("Podano nieprawidłowy typ czujnika.");
                    err = true;
                    break;
            }
            if (err)
                Console.WriteLine("Podczas dodawania czujnika wystąpił błąd!");
            else Console.WriteLine("Dodano czujnik!");
        }
        void AddSensor(string name, string sensorName)
        {
            switch (name.ToLower())
            {
                case "temperature":
                    sensors.Add(new TemperatureSensor(sensorName));
                    Console.WriteLine("Dodano czujnik!");
                    break;
                case "humidity":
                    sensors.Add(new HumiditySensor(sensorName));
                    Console.WriteLine("Dodano czujnik!");
                    break;
                case "pressure":
                    sensors.Add(new PressureSensor(sensorName));
                    Console.WriteLine("Dodano czujnik!");
                    break;
                case "tempandhumidity":
                    sensors.Add(new TemperatureAndHumiditySensor(sensorName));
                    Console.WriteLine("Dodano czujnik!");
                    break;
                default:
                    Console.WriteLine("Podano nieprawidłowy typ czujnika.");
                    break;
            }
        }

        internal Func<double, bool> IsGreaterThanValue;
        internal Func<double, bool> IsLowerThanValue;
        public void GetSpecifiedData(string type, string comparsionString, double value)
        {
            IsGreaterThanValue = x => x > value;
            IsLowerThanValue = x => x < value;
            Console.WriteLine(type + " " + comparsionString + " " + value);
            switch (type)
            {
                case "t":
                    foreach (Sensor sensor in sensors)
                        if (sensor is ITemperature)
                        {
                            ITemperature ts = sensor as ITemperature; 
                            if (comparsionString.Equals(">"))
                            {
                                if (IsGreaterThanValue(ts.Temperature))
                                    Console.WriteLine("{0}: {1} {2}",sensor.Name, ts.Temperature, ts.Unit);
                            }
                            else
                            {
                                if (IsLowerThanValue(ts.Temperature))
                                    Console.WriteLine("{0}: {1} {2}", sensor.Name, ts.Temperature, ts.Unit);
                            }
                        }
                    break;
                case "h":
                    foreach (Sensor sensor in sensors)
                        if (sensor is IHumidity)
                        {
                            IHumidity hs = sensor as IHumidity;
                            if (comparsionString.Equals(">"))
                            {
                                if (IsGreaterThanValue(hs.Humidity))
                                    Console.WriteLine("{0}: {1}%",sensor.Name, hs.Humidity);
                            }
                            else
                            {
                                if (IsLowerThanValue(hs.Humidity))
                                    Console.WriteLine("{0}: {1}%", sensor.Name, hs.Humidity);
                            }
                        }
                    break;
                case "p":
                    foreach (Sensor sensor in sensors)
                        if (sensor is IPressure)
                        {
                            IPressure ps = sensor as IPressure;
                            if (comparsionString.Equals(">"))
                            {
                                if (IsGreaterThanValue(ps.Pressure))
                                    Console.WriteLine("{0}: {1} hPa", sensor.Name, ps.Pressure);
                            }
                            else
                            {
                                if (IsLowerThanValue(ps.Pressure))
                                    Console.WriteLine("{0}: {1} hPA", sensor.Name, ps.Pressure);
                            }
                        }
                    break;
            }
        }

        public void GetAllDataByType(char typeOfSensor)
        {
            switch (typeOfSensor)
            {
                case 't':
                    foreach (Sensor sensor in sensors)
                        if (sensor is ITemperature)
                        {
                            ITemperature ts = sensor as ITemperature;
                            ts.Unit = unit;
                            Console.WriteLine("{0}: {1} {2}", sensor.Name, ts.Temperature, ts.Unit);
                        }
                    break;
                case 'h':
                    foreach (Sensor sensor in sensors)
                        if (sensor is IHumidity)
                        {
                            IHumidity hs = sensor as IHumidity;
                            Console.WriteLine("{0}: {1}%", sensor.Name, hs.Humidity);
                        }
                    break;
                case 'p':
                    foreach (Sensor sensor in sensors)
                        if (sensor is IPressure)
                        {
                            IPressure ps = sensor as IPressure;
                            Console.WriteLine("{0}: {1} hPA", sensor.Name, ps.Pressure);
                        }
                    break;
                default:
                    throw new Exception("Wrong switch case!");
            }
        }

    }
}
