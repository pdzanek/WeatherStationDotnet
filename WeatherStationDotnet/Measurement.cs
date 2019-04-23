using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherStationDotnet
{
    class Measurement
    {
        string key;
        double value;
        Measurement(string key, double value)
        {
            this.key = key;
            this.value = value;
        }
    }
}
