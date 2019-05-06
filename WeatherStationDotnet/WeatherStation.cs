using System;
using System.Collections.Generic;

namespace WeatherStationDotnet
{
    class WeatherStation
    {
        private Sensor sensor;
        static List<Sensor> sensors;
        string Name;
        static char unit = 'C';
        public WeatherStation(string WeatherStationName)
        {
            Name = WeatherStationName;
            sensors = new List<Sensor>();
        }
        void eventHandlerPrinter(Measurement measurement)
        {
            Console.WriteLine(measurement.Key + " " + measurement.Value);
        }
        public void AddSensor(string name)
        {
            bool err = false;
            switch (name.ToLower())
            {
                case "temperature":
                    sensor = new TemperatureSensor();
                    sensor.MeasurementEvent += eventHandlerPrinter;
                    Program.sensors.Add(sensor);
                    Console.WriteLine("Dodano czujnik!");
                    break;
                case "humidity":
                    sensor = new HumiditySensor();
                    sensor.MeasurementEvent += eventHandlerPrinter;
                    Console.WriteLine("Dodano czujnik!");
                    break;
                case "pressure":
                    sensor = new PressureSensor();
                    sensor.MeasurementEvent += eventHandlerPrinter;
                    Console.WriteLine("Dodano czujnik!");
                    break;
                case "tempandhumidity":
                    sensor = new TemperatureAndHumiditySensor();
                    sensor.MeasurementEvent += eventHandlerPrinter;
                    Console.WriteLine("Dodano czujnik!");
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
        public void AddSensor(string name, string sensorName)
        {
            switch (name.ToLower())
            {
                case "temperature":
                    sensor = new TemperatureSensor(sensorName);
                    sensor.MeasurementEvent += eventHandlerPrinter;
                    Console.WriteLine("Dodano czujnik!");
                    break;
                case "humidity":
                    sensor = new HumiditySensor(sensorName);
                    sensor.MeasurementEvent += eventHandlerPrinter;
                    Console.WriteLine("Dodano czujnik!");
                    break;
                case "pressure":
                    sensor = new PressureSensor(sensorName);
                    sensor.MeasurementEvent += eventHandlerPrinter;
                    Console.WriteLine("Dodano czujnik!");
                    break;
                case "tempandhumidity":
                    sensor = new TemperatureAndHumiditySensor(sensorName);
                    sensor.MeasurementEvent += eventHandlerPrinter;
                    Console.WriteLine("Dodano czujnik!");
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
        private bool CheckIfSensorExists(string name)
        {
            bool exists = false;
            foreach (Sensor sensor in Program.sensors)
            {
                if (name.Equals(sensor.Name))
                {
                    Console.WriteLine(sensor.Name);
                    sensor.MeasurementEvent += eventHandlerPrinter;
                    exists = true;
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
