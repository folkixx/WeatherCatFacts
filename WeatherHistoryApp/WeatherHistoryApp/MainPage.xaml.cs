﻿using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WeatherHistoryApp
{
    public partial class MainPage : ContentPage
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnShowInfoClicked(object sender, EventArgs e)
        {
            string city = cityEntry.Text;
            weatherLabel.Text = $"Погода в {city}: " + await GetWeatherAsync(city);
            factLabel.Text = "Интересный факт о кошках: " + await GetCatFactAsync();
        }

        private async Task<string> GetWeatherAsync(string city)
        {
            var apiKey = "6eb0cf7d36959f9da6bef8ffa372be75";
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric&lang=ru";

            try
            {
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var weather = JsonConvert.DeserializeObject<WeatherResponse>(content);
                    return $"{weather.Main.Temp}°C и {weather.Weather[0].Description}";
                }
                else
                {
                    return "Не удалось получить данные о погоде.";
                }
            }
            catch (Exception ex)
            {
                return $"Ошибка: {ex.Message}";
            }
        }

        private async Task<string> GetCatFactAsync()
        {
            string url = "https://catfact.ninja/fact";

            try
            {
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var fact = JsonConvert.DeserializeObject<CatFact>(content);
                    return fact.Fact;
                }
                else
                {
                    return "Не удалось получить факт о кошках.";
                }
            }
            catch (Exception ex)
            {
                return $"Ошибка: {ex.Message}";
            }
        }
    }

    // Классы для десериализации JSON-ответов
    public class WeatherResponse
    {
        public Main Main { get; set; }
        public Weather[] Weather { get; set; }
    }

    public class Main
    {
        public double Temp { get; set; }
    }

    public class Weather
    {
        public string Description { get; set; }
    }

    // Класс для десериализации ответа от Cat Facts API
    public class CatFact
    {
        public string Fact { get; set; }
        public int Length { get; set; }
    }
}
