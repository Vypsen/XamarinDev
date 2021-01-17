using System;
using System.Collections.Generic;
using System.Text;

namespace weather
{

  
   
    public class WeatherCurrent
    {
        public WeatherInfoCoord coord { get; set; }
        public WeatherInfoWeather[] weather { get; set; }
        public WeatherInfoMain main { get; set; }
        public WeatherInfoWind wind { get; set; }
        public int dt { get; set; }
        public string name { get; set; }
    }

  

    public class WeatherInfoMain
    {
        public float temp { get; set; }
        public float pressure { get; set; }
        public int humidity { get; set; }
        public float temp_min { get; set; }
        public float temp_max { get; set; }

    }

    public class WeatherInfoWind
    {
        public float speed { get; set; }
        public float deg { get; set; }
    }

    public class WeatherInfoWeather
    {
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class WeatherInfoCoord
    {
        public float lat { get; set; }
        public float lon { get; set; }
    }
}
