﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace WeatherStationDotnet
{
    class WeatherStation
    {
        public bool active = false;
        private Sensor sensor;
        static List<KeyValuePair<string,double>> measurements;
        static List<Sensor> sensors;
        public string Name { get; set; }
        static char unit = 'C';
        public WeatherStation(string WeatherStationName)
        {
            Name = WeatherStationName;
            measurements = new List<KeyValuePair<string,double>>();
        }
        void eventHandlerPrinter(Measurement measurement)
        {
            bool added = false;
            if (active)
            {
                string[] words = measurement.kvp.Key.Split(' ');
                for(int i=0;i<measurements.Count;i++)
                {
                    string[] elem_words = measurements[i].Key.Split(' ');
                    if (words[0].Equals(elem_words[0]) && words[1].Equals(elem_words[1]))
                    {
                        measurements[i] =measurement.kvp;
                        added = true;
                    }
                }
                //Console.WriteLine("{0}: {1} {2}", Name,measurement.kvp.Key, measurement.kvp.Value);
                if(!added)
                measurements.Add(measurement.kvp);
            }
        }

        public void AddSensor(string type, string sensorName)
        {
            switch (type.ToLower())
            {
                case "temperature":
                    if (!CheckIfSensorExists(sensorName))
                    {
                        sensor = new TemperatureSensor(sensorName);
                        Program.sensors.Add(sensor);
                        sensor.MeasurementEvent += eventHandlerPrinter;
                        Console.WriteLine("Dodano czujnik!");
                    }
                    else
                        Console.WriteLine("Zasubskrybowano istniejący czujnik.");
                    break;
                case "humidity":
                    if (!CheckIfSensorExists(sensorName))
                    {
                        sensor = new HumiditySensor(sensorName);
                        Program.sensors.Add(sensor);
                        sensor.MeasurementEvent += eventHandlerPrinter;
                        Console.WriteLine("Dodano czujnik!");
                    }
                    else
                        Console.WriteLine("Zasubskrybowano istniejący czujnik.");
                    break;
                case "pressure":
                    if (!CheckIfSensorExists(sensorName))
                    {
                        sensor = new PressureSensor(sensorName);
                        Program.sensors.Add(sensor);
                        sensor.MeasurementEvent += eventHandlerPrinter;
                        Console.WriteLine("Dodano czujnik!");
                    }
                    else
                        Console.WriteLine("Zasubskrybowano istniejący czujnik.");
                    break;
                case "tempandhumidity":
                    if (!CheckIfSensorExists(sensorName))
                    {
                        sensor = new TemperatureAndHumiditySensor(sensorName);
                        Program.sensors.Add(sensor);
                        sensor.MeasurementEvent += eventHandlerPrinter;
                        Console.WriteLine("Dodano czujnik!");
                    }
                    else
                        Console.WriteLine("Zasubskrybowano istniejący czujnik.");
                    break;
                default:
                    Console.WriteLine("Podano nieprawidłowy typ czujnika.");
                    break;
            }
        }

        public void SetUnit(char value) => unit = value;

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
                                    Console.WriteLine("{0}: {1} {2}", sensor.Name, ts.Temperature, ts.Unit);
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
                                    Console.WriteLine("{0}: {1}%", sensor.Name, hs.Humidity);
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
                    foreach (KeyValuePair<string, double> kvp in measurements)
                    {
                        string[] words = kvp.Key.Split(' ');
                        if (words[1].Equals("temperature"))
                        {
                            Console.WriteLine("{0}: {1} {2}", words[0], kvp.Value, words[2]);
                        }
                    }
                    break;
                case 'h':
                    foreach (KeyValuePair<string, double> kvp in measurements)
                    {
                        string[] words = kvp.Key.Split(' ');
                        if (words[1].Equals("humidity"))
                        {
                            Console.WriteLine("{0}: {1}%", kvp.Key, kvp.Value);
                        }
                    }
                    break;
                case 'p':
                    foreach(KeyValuePair<string, double> kvp in measurements)
                    {
                        string[] words = kvp.Key.Split(' ');
                        if (words[1].Equals("pressure"))
                        {
                            Console.WriteLine("{0}: {1} hPa", kvp.Key, kvp.Value);
                        }
                    }
                    break;
                default:
                    throw new Exception("Wrong switch case!");
            }
        }
        private bool CheckIfSensorExists(string name)
        {
            bool exists = false;
            if (Program.sensors.Count != 0 && Program.sensors != null)
            {
                foreach (Sensor sensor in Program.sensors)
                {
                    if (name.Equals(sensor.Name))
                    {
                        Console.WriteLine(sensor.Name);
                        sensor.MeasurementEvent += eventHandlerPrinter;
                        exists = true;
                    }
                }
            }
            return exists;
        }

    }
}
        /* public void SerializeData()
         {
             string type;
             var output = File.OpenWrite("log.json");

             while (true)
             {
                 if (sensors.Count != 0 && sensors != null)
                 {
                     DataContractJsonSerializer dateWriter = new DataContractJsonSerializer(typeof(string));
                     dateWriter.WriteObject(output, DateTime.Now.ToString("h:mm:ss"));

                     foreach (Sensor sensor in sensors)
                     {
                         type = (sensor.GetType().Name);
                         switch (type)
                         {
                             case "TemperatureAndHumiditySensor":
                                 TemperatureAndHumiditySensor tempTAH = (TemperatureAndHumiditySensor)sensor;
                                 tempTAH.Humidity++;
                                 tempTAH.Temperature++;
                                 break;
                             case "TemperatureSensor":
                                 TemperatureSensor tempT = (TemperatureSensor)sensor;
                                 tempT.Temperature++;
                                 break;
                             case "HumiditySensor":
                                 HumiditySensor tempH = (HumiditySensor)sensor;
                                 tempH.Humidity++;
                                 break;
                             case "PressureSensor":
                                 PressureSensor tempP = (PressureSensor)sensor;
                                 tempP.Pressure++;
                                 break;
                         }
                         DataContractJsonSerializer writer = new DataContractJsonSerializer(sensor.GetType());
                         writer.WriteObject(output, sensor);
                     }
                 }
                 Thread.Sleep(1000);
             }
         }
     }
     */
