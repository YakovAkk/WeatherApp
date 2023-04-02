using System.Collections.Generic;

namespace WeatherApp.Model
{
    public class WeatherModel
    {
        public Coord Coord { get; set; }
        public List<Weather> Weather { get; set; }
        public Main Main { get; set; }
        public float Visibility { get; set; }
        public Wind Wind { get; set; }

    }
    public class Coord
    {
        public float Lon { get; set; }
        public float Lat { get; set; }
    }

    public class Weather
    {
        public string Main { get; set; }
        public string Description { get; set; }
    }
    public class Main
    {
        public float Temp { get; set; }
        public float Feels_like { get; set; }
        public float Temp_min { get; set; }
        public float Temp_max { get; set; }
        public float Pressure { get; set; }
        public float Humidity { get; set; }
    }

    public class Wind
    {
        public float Speed { get; set; }
        public float Deg { get; set; }
    }
}
