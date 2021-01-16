using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace weather
{
    public partial class MainPage : ContentPage
    {
        static WeatherInfo weatherInfo = new WeatherInfo();
        static WeatherFor7Days weatherFor7Days = new WeatherFor7Days();
        static double lat;
        static double lon;
        static int currentDayCounter = -1;
        static bool requestAccepted = false;
        
        public MainPage()
        {
            InitializeComponent();

            getGeo();
        }



        

        async void getGeo()
        {
            double lat;
            double lon;

            var result = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromSeconds(1)));

            lat = result.Latitude;
            lon = result.Longitude;

            Console.WriteLine(lat);
            Console.WriteLine(lon);

            string geoloc = $"lat={lat}&lon={lon}";


            doRequest(geoloc);

            currentWeather();
        }



        private void doRequest(string geoloc)
        {
            

            string url = $"http://api.openweathermap.org/data/2.5/weather?{geoloc}&units=metric&lang=ru&appid=9225c75831855f6460fb93f2152a5396";
            string response;

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }

            weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(response);

            lat = weatherInfo.coord.lat;
            lon = weatherInfo.coord.lon;

            url = $"https://api.openweathermap.org/data/2.5/onecall?lat={lat}&lon={lon}&lang=ru&units=metric&appid=9225c75831855f6460fb93f2152a5396";

            httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }

            weatherFor7Days = JsonConvert.DeserializeObject<WeatherFor7Days>(response);
        }

        private void currentWeather()
        {

            //backImg.Source = getImg(weatherInfo.weather[0].main);
            backImg.Source = $"{weatherInfo.weather[0].main}.jpg";
            string icon = weatherInfo.weather[0].icon;

            iconWeather.Source = $"https://openweathermap.org/img/wn/{icon}@2x.png";

            CitySearch.Text = weatherInfo.name;
            requestAccepted = true;
            cityLabel.Text = weatherInfo.name;
            tempLabel.Text = $"{((int)weatherInfo.main.temp)} °C";
            weatherDescriptionLabel.Text = weatherInfo.weather[0].description;
            windSpeedLabel.Text = $"Скорость ветра:  { weatherInfo.wind.speed} м/с";
            humidityLabel.Text = $"Влажность:  {weatherInfo.main.humidity} % ";
            pressureLabel.Text = $"Атм. давление:  {weatherInfo.main.pressure} мм.рт.ст.";
            minMaxTemp.Text = $" {(int)weatherFor7Days.daily[0].temp.min} °C / {(int)weatherFor7Days.daily[0].temp.max} °C";
           
   
        }

        private void CitySearch_Completed(object sender, EventArgs e)
        {
            Entry entry = sender as Entry;
            try
            {

                doRequest($"q={entry.Text}");
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
                if (currentDayCounter == -1)
                {
                    currentWeather();
                }
                else
                {
                    //getImg(weatherInfo.weather[0].main);
                    backImg.Source = $"{weatherFor7Days.daily[currentDayCounter].weather[0].main}.jpg";

                    string icon = weatherFor7Days.daily[currentDayCounter].weather[0].icon;

                    iconWeather.Source = $"https://openweathermap.org/img/wn/{icon}@2x.png";

                    cityLabel.Text = weatherInfo.name;
                    tempLabel.Text = $" {((int)weatherFor7Days.daily[currentDayCounter].temp.day)}°C";
                    weatherDescriptionLabel.Text = weatherFor7Days.daily[currentDayCounter].weather[0].description;
                    windSpeedLabel.Text = $"Скорость ветра: {weatherFor7Days.daily[currentDayCounter].wind_speed} м/с ";
                    humidityLabel.Text = $"Влажность: {weatherFor7Days.daily[currentDayCounter].humidity} % ";
                    pressureLabel.Text = $"Атм. давление: {weatherFor7Days.daily[currentDayCounter].pressure} мм.рт.ст. ";
                    minMaxTemp.Text = $" {(int)weatherFor7Days.daily[currentDayCounter].temp.min} °C / {(int)weatherFor7Days.daily[currentDayCounter].temp.max} °C";
                    
                }
            }

            switch (currentDayCounter)
            {
                case -1:
                    {
                        status.Text = "Прямо сейчас";
                        break;
                    }

                case 1:
                    {
                        status.Text = "Завтра";
                        break;
                    }

                case 2:
                    {
                        status.Text = "Послезавтра";
                        break;
                    }

                default:
                    {
                        status.Text = "Другой день";
                        break;
                    }
            }
        }

        private void Swiped(object sender, SwipedEventArgs e)
        {
            Console.WriteLine(e.Direction);
            switch (e.Direction)
            {
                case SwipeDirection.Right:
                    try
                    {

                        
                        Console.WriteLine(currentDayCounter);
                        if (currentDayCounter == 1)
                        {
                            currentDayCounter -= 2;
                        }

                        else if (currentDayCounter != -1)
                        {
                            currentDayCounter--;
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
                        if (currentDayCounter == -1)
                        {
                            currentDayCounter += 2;
                        }

                        else if (currentDayCounter != 6)
                        {
                            currentDayCounter++;
                 
                        }
                        Console.WriteLine(currentDayCounter);

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
