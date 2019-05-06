namespace WeatherStationDotnet
{
    public class Measurement
    {
       public Measurement(string key, double value)
        {
            Key = key;
            Value = value;
        }
        public string Key { get; set; }
        public double Value { get; set; }
    }
}