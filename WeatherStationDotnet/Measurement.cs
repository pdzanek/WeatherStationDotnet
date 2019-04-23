
namespace WeatherStationDotnet
{
    class Measurement
    {
        Measurement(string key, double value)
        {
            Key = key;
            Value = value;
        }
        string Key { get; set; }
        double Value { get; set; }
    }
}
