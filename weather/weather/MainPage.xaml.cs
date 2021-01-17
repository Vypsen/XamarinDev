using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Threading;

namespace weather
{
    public partial class MainPage : ContentPage
    {
        WeatherCurrent weatherCurrent = new WeatherCurrent();
        WeatherWeek weatherWeek = new WeatherWeek();
        double lat;
        double lon;
        string icon;
        int currentDay = -1;
        string url;
        string response;
        bool requestAccepted = false;
        
        public MainPage()
        {
            InitializeComponent();

            getGeo();
        }

        async void getGeo()
        {
            try
            {
                var CurrentLocation = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromSeconds(1)));

                lat = CurrentLocation.Latitude;
                lon = CurrentLocation.Longitude;

                Console.WriteLine(lat);
                Console.WriteLine(lon);

                var geoloc = $"lat={lat}&lon={lon}";


                GetData(geoloc);

                currentWeather();
            }
            catch
            {
                
            }
        }


        private void GetData(string geoloc)
        {
            

            url = $"http://api.openweathermap.org/data/2.5/weather?{geoloc}&units=metric&lang=ru&appid=9225c75831855f6460fb93f2152a5396";
           

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }

            weatherCurrent = JsonConvert.DeserializeObject<WeatherCurrent>(response);

            lat = weatherCurrent.coord.lat;
            lon = weatherCurrent.coord.lon;

            url = $"https://api.openweathermap.org/data/2.5/onecall?lat={lat}&lon={lon}&lang=ru&units=metric&appid=9225c75831855f6460fb93f2152a5396";

            httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }

            weatherWeek = JsonConvert.DeserializeObject<WeatherWeek>(response);
        }

        private void currentWeather()
        {

            backImg.Source = $"{weatherCurrent.weather[0].main}.jpg";
            icon = weatherCurrent.weather[0].icon;

            iconWeather.Source = $"https://openweathermap.org/img/wn/{icon}@2x.png";

            CitySearch.Text = weatherCurrent.name;
            requestAccepted = true;
            cityLabel.Text = weatherCurrent.name;
            tempLabel.Text = $"{((int)weatherCurrent.main.temp)} °C";
            descriptionLabel.Text = weatherCurrent.weather[0].description;
            windSpeedLabel.Text = $"Скорость ветра:  { weatherCurrent.wind.speed} м/с";
            humidityLabel.Text = $"Влажность:  {weatherCurrent.main.humidity} % ";
            pressureLabel.Text = $"Атм. давление:  {weatherCurrent.main.pressure} мм.рт.ст.";
            minMaxTemp.Text = $" {(int)weatherWeek.daily[0].temp.min} °C / {(int)weatherWeek.daily[0].temp.max} °C";

            var Date = getDateTime(weatherCurrent.dt).ToString();
            dateTime.Text = Date.Substring(0, Date.Length - 3);


        }

        private void CitySearch_Completed(object sender, EventArgs e)
        {
            Entry entry = sender as Entry;
            try
            {
                entry.Text = entry.Text.Trim(' ');
                GetData($"q={entry.Text}");
                requestAccepted = true;
                currentWeather();
            }
            catch {
                
                DisplayAlert("Ошибка", "Неверный ввод", "ОK");
            }
        }

        
     
        private void refreshInfo()
        {
            if (requestAccepted)
            {
                if (currentDay == -1)
                {
                    currentWeather();
                }
                else
                {
                    backImg.Source = $"{weatherWeek.daily[currentDay].weather[0].main}.jpg";

                    icon = weatherWeek.daily[currentDay].weather[0].icon;

                    iconWeather.Source = $"https://openweathermap.org/img/wn/{icon}@2x.png";

                    cityLabel.Text = weatherCurrent.name;
                    tempLabel.Text = $" {((int)weatherWeek.daily[currentDay].temp.day)}°C";
                    descriptionLabel.Text = weatherWeek.daily[currentDay].weather[0].description;    
                    windSpeedLabel.Text = $"Скорость ветра: {weatherWeek.daily[currentDay].wind_speed} м/с ";
                    humidityLabel.Text = $"Влажность: {weatherWeek.daily[currentDay].humidity} % ";
                    pressureLabel.Text = $"Атм. давление: {weatherWeek.daily[currentDay].pressure} мм.рт.ст. ";
                    minMaxTemp.Text = $" {(int)weatherWeek.daily[currentDay].temp.min} °C / {(int)weatherWeek.daily[currentDay].temp.max} °C";

                    var Date = getDateTime(weatherWeek.daily[currentDay].dt).ToString();
                    dateTime.Text = Date.Substring(0, Date.Length - 8);
                }
            }

        }

        private DateTime getDateTime(int UnixTime)
        {

            return new DateTime(1970, 1, 1, 10, 0, 0).AddSeconds(UnixTime);
        }



        private void Swiped(object sender, SwipedEventArgs e)
        {
            Console.WriteLine(e.Direction);
            switch (e.Direction)
            {
                case SwipeDirection.Right:
                    try
                    {
                        Console.WriteLine(currentDay);
                        if (currentDay == 1)
                        {
                            currentDay -= 2;
                        }

                        else if (currentDay != -1)
                        {
                            currentDay--;
                        }
                        refreshInfo();
                    }
                    catch
                    {
                        DisplayAlert("Ошибка", "Неверный ввод", "ОK");
                    };

                    

                    break;
                case SwipeDirection.Left:
                    try
                    {
                        if (currentDay == -1)
                        {
                            currentDay += 2;
                        }

                        else if (currentDay != 6)
                        {
                            currentDay++;
                 
                        }
                        Console.WriteLine(currentDay);

                        refreshInfo();

                    }
                    catch
                    {
                        DisplayAlert("Ошибка", "Неверный ввод", "ОK");
                    };

                    
                
                    break;

            }

        }
    }
}
