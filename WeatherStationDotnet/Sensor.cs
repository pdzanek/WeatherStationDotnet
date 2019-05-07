using System;
using System.Runtime.Serialization;

namespace WeatherStationDotnet
{
    [DataContract]
    public class Sensor
    {
        public delegate void MeasurementDelegate(Measurement measurement);
        public event MeasurementDelegate MeasurementEvent;
        [DataMember]
        public string Name { set; get; }
        public static int instances = 0;
        public Sensor()
        {
            Name = "Sensor" + instances;
            instances++;
        }
        public Sensor(string name)
        {
            if (name.Length < 1) name = "Sensor";
            try
            {
                if (name.Length >= 16)
                {
                    string shortName = name.Substring(0, 16);
                    Name = shortName + instances;
                    instances++;
                    throw new Exception("Podana nazwa sensora jest dłuższa niż 16 znaków, użyto skróconej nazwy.\n");
                }
                else
                {
                    Name = name + instances;
                    instances++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void Measurement(string key, double value)
        {
            MeasurementEvent?.Invoke(new Measurement(key, value));
        }
    }
}