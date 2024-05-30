# WeatherHistoryApp

## Описание

WeatherHistoryApp - это мобильное приложение на Xamarin.Forms, которое предоставляет пользователю текущую информацию о погоде в указанном городе и случайный интересный факт о кошках. 

## Задачи

Приложение выполняет следующие задачи:
- Получение и отображение текущей информации о погоде в выбранном пользователем городе.
- Получение и отображение случайного интересного факта о кошках.

## Технические особенности

### Основные функции

Приложение использует два основных метода для получения данных:
- `GetWeatherAsync`: Получает данные о текущей погоде для указанного города с помощью API OpenWeatherMap.
- `GetCatFactAsync`: Получает случайный факт о кошках с помощью API Cat Fact Ninja.

### Реализация

Приложение написано на языке C# с использованием Xamarin.Forms и библиотеки Newtonsoft.Json для обработки JSON-ответов от API. Ниже приведен код функций и описание их работы.

#### Код функций

##### Метод для получения погоды

```csharp
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

##### Метод для получения факта о кошках
```csharp private async Task<string> GetCatFactAsync()
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
