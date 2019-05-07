using System.Collections.Generic;

namespace WeatherStationDotnet
{
    public class Measurement
    {
       public KeyValuePair<string, double> kvp;
       public Measurement(string key, double value)
        {
             kvp= new KeyValuePair<string, double>(key,value);
        }
    }
}