using System;

namespace WeatherStationDotnet
{
    public class Sensor
    {
        public string Name { set; get; }
        public static int instances = 0;
        public Sensor()
        {
            Name = "Sensor" + instances;
            instances++;
        }
        public Sensor(string name)
        {
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
    }
}