using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading;

namespace WeatherStationDotnet
{
    class WeatherStation
    {
        public bool active = false;
        private Sensor sensor;
        List<KeyValuePair<string,double>> measurements;
        public string Name { get; set; }
        private static readonly char unit = 'C';
        public WeatherStation(string WeatherStationName)
        {
            Name = WeatherStationName;
            measurements = new List<KeyValuePair<string, double>>();
            Thread serializerThread = new Thread(SerializeData);
            serializerThread.Start();
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
                    foreach (KeyValuePair<string, double> kvp in measurements)
                    {
                        string[] words = kvp.Key.Split(' ');
                        if (words[1].Equals("temperature"))
                        {
                            if (comparsionString.Equals(">"))
                            {
                                if (IsGreaterThanValue(kvp.Value))
                                    Console.WriteLine("{0}: {1} {2}", words[0], kvp.Value, words[2]);
                            }
                            else
                            {
                                if (IsLowerThanValue(kvp.Value))
                                    Console.WriteLine("{0}: {1} {2}", words[0], kvp.Value, words[2]);
                            }
                        }
                    }
                    break;
                case "h":
                    foreach (KeyValuePair<string, double> kvp in measurements)
                    {
                        string[] words = kvp.Key.Split(' ');
                        if (words[1].Equals("humidity"))
                        {
                            if (comparsionString.Equals(">"))
                            {
                                if (IsGreaterThanValue(kvp.Value))
                                    Console.WriteLine("{0}: {1}%", words[0], kvp.Value);
                            }
                            else
                            {
                                if (IsLowerThanValue(kvp.Value))
                                    Console.WriteLine("{0}: {1}%", words[0], kvp.Value);
                            }
                        }
                    }
                    break;
                case "p":
                    foreach (KeyValuePair<string, double> kvp in measurements)
                    {
                        string[] words = kvp.Key.Split(' ');
                        if (words[1].Equals("pressure"))
                        {
                            if (comparsionString.Equals(">"))
                            {
                                if (IsGreaterThanValue(kvp.Value))
                                    Console.WriteLine("{0}: {1} hPa", words[0], kvp.Value);
                            }
                            else
                            {
                                if (IsLowerThanValue(kvp.Value))
                                    Console.WriteLine("{0}: {1} hPa", words[0], kvp.Value);
                            }
                        }
                    }
                    break;
            }
        }
        public void GetAllDataByType(char typeOfSensor)
        {
            switch (typeOfSensor)
            {
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
        public void GetAllDataByType(char TypeOfSensor, char tempUnit)
        {
           foreach (KeyValuePair<string, double> kvp in measurements)
            {
                string[] words = kvp.Key.Split(' ');
                if (words[1].Equals("temperature"))
                {
                    if(tempUnit==words[2][0])
                        Console.WriteLine("{0}: {1} {2}", words[0], kvp.Value, words[2]);
                    else 
                        if(tempUnit.Equals('F')) Console.WriteLine("{0}: {1} {2}", words[0], kvp.Value* 9 / 5 + 32, 'F');
                        else Console.WriteLine("{0}: {1} {2}", words[0], (kvp.Value-32) * 5 / 9, 'C');
                }
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
        public void SerializeData()
        {
            var output = File.OpenWrite("log"+Name+".json");

             while (true)
              {
                if (measurements.Count != 0 && measurements != null)
                {
                    DataContractJsonSerializer dateWriter = new DataContractJsonSerializer(typeof(string));
                    dateWriter.WriteObject(output, DateTime.Now.ToString("h:mm:ss"));

                    foreach (KeyValuePair<string,double> kvp in measurements)
                    {
                        DataContractJsonSerializer writer = new DataContractJsonSerializer(kvp.GetType());
                        writer.WriteObject(output, kvp);
                    }
                }
                Thread.Sleep(5000);
            }
        }
    }
   
}
