## Описание приложения

Наше приложение - это погодное историческое приложение, которое позволяет пользователям получать информацию о текущей погоде в заданном городе и интересные факты о кошках.

### Задачи приложения:

- Показывать текущую погоду в указанном городе.
- Предоставлять интересные факты о кошках.

## Технические особенности

Приложение разработано с использованием языка программирования C# и фреймворка Xamarin.Forms. Оно использует HTTP-запросы для получения данных о погоде из API OpenWeatherMap и фактов о кошках из API Cat Facts.

### Код функции получения погоды, а так же факта о кошках:

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
