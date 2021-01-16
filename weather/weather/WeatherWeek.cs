using System;
using System.Collections.Generic;
using System.Text;

namespace weather
{
    public class WeatherFor7Days
    {
        public WeatherForDay[] daily { get; set; }
    }

        
  
    public class WeatherForDay
    {
        public WeatherForDayTemp temp { get; set; }
        public float pressure { get; set; }
        public int humidity { get; set; }
        public float wind_speed { get; set; }
        public WeatherForDayWeather[] weather { get; set; }
    }

    public class WeatherForDayTemp
    {
        public float day { get; set; }
        public float min { get; set; }
        public float max { get; set; }
    }

    public class WeatherForDayWeather
    {
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }
    

}
