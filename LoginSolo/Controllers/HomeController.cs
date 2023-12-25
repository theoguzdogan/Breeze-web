using LoginSolo.Entities;
using LoginSolo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace LoginSolo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        private async Task<List<City>> InitCitiesList(AppUser currentUser)
        {
            var citiesList = new List<City>();
            if (currentUser != null)
            {
                if (currentUser.Cities != null)
                {
                    foreach (var cityName in currentUser.Cities.Split('.'))
                    {
                        try
                        {
                            string weatherDataJson = await FetchWeatherData(cityName);
                            var weatherResponse = JsonSerializer.Deserialize<JsonElement>(weatherDataJson);
                            int currentHour = Int32.Parse(weatherResponse.GetProperty("location").GetProperty("localtime").GetString().Split(' ')[1].Split(':')[0]);
                            //string currentDate = DateTime.Today.DayOfWeek.ToString();
                            string currentDate = DateTime.Parse(weatherResponse.GetProperty("location").GetProperty("localtime").GetString().Split(' ')[0]).DayOfWeek.ToString();
                            citiesList.Add(new City()
                            {
                                name = weatherResponse.GetProperty("location").GetProperty("name").GetString(),
                                temp_c = weatherResponse.GetProperty("current").GetProperty("temp_c").GetDouble().ToString(),
                                feelslike_c = weatherResponse.GetProperty("current").GetProperty("feelslike_c").GetDouble().ToString(),
                                humidity = weatherResponse.GetProperty("current").GetProperty("humidity").GetDouble().ToString(),
                                precipitation_mm = weatherResponse.GetProperty("current").GetProperty("precip_mm").GetDouble().ToString(),
                                condition_text = weatherResponse.GetProperty("current").GetProperty("condition").GetProperty("text").GetString(),
                                condition_icon = weatherResponse.GetProperty("current").GetProperty("condition").GetProperty("icon").GetString(),
                                min_c = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[0].GetProperty("day").GetProperty("mintemp_c").ToString(),
                                avg_c = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[0].GetProperty("day").GetProperty("avgtemp_c").ToString(),
                                max_c = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[0].GetProperty("day").GetProperty("maxtemp_c").ToString(),
                                current_date = currentDate,
                                current_hour = currentHour.ToString(),
                                hour1_c = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[0].GetProperty("hour")[currentHour + 1].GetProperty("temp_c").ToString(),
                                hour2_c = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[0].GetProperty("hour")[currentHour + 2].GetProperty("temp_c").ToString(),
                                hour3_c = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[0].GetProperty("hour")[currentHour + 3].GetProperty("temp_c").ToString(),
                                hour4_c = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[0].GetProperty("hour")[currentHour + 4].GetProperty("temp_c").ToString(),
                                hour5_c = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[0].GetProperty("hour")[currentHour + 5].GetProperty("temp_c").ToString(),
                                sunrise = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[0].GetProperty("astro").GetProperty("sunrise").GetString(),
                                sunset = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[0].GetProperty("astro").GetProperty("sunset").GetString(),


                                day1_weekday = DateTime.Parse(weatherResponse.GetProperty("forecast").GetProperty("forecastday")[1].GetProperty("date").GetString()).DayOfWeek.ToString(),
                                day1_min_c = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[1].GetProperty("day").GetProperty("mintemp_c").ToString(),
                                day1_avg_c = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[1].GetProperty("day").GetProperty("avgtemp_c").ToString(),
                                day1_max_c = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[1].GetProperty("day").GetProperty("maxtemp_c").ToString(),
                                day1_cond_text = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[1].GetProperty("day").GetProperty("condition").GetProperty("text").GetString(),
                                day1_cond_icon = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[1].GetProperty("day").GetProperty("condition").GetProperty("icon").GetString(),

                                day2_weekday = DateTime.Parse(weatherResponse.GetProperty("forecast").GetProperty("forecastday")[2].GetProperty("date").GetString()).DayOfWeek.ToString(),
                                day2_min_c = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[2].GetProperty("day").GetProperty("mintemp_c").ToString(),
                                day2_avg_c = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[2].GetProperty("day").GetProperty("avgtemp_c").ToString(),
                                day2_max_c = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[2].GetProperty("day").GetProperty("maxtemp_c").ToString(),
                                day2_cond_text = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[2].GetProperty("day").GetProperty("condition").GetProperty("text").GetString(),
                                day2_cond_icon = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[2].GetProperty("day").GetProperty("condition").GetProperty("icon").GetString(),

                                day3_weekday = DateTime.Parse(weatherResponse.GetProperty("forecast").GetProperty("forecastday")[3].GetProperty("date").GetString()).DayOfWeek.ToString(),
                                day3_min_c = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[3].GetProperty("day").GetProperty("mintemp_c").ToString(),
                                day3_avg_c = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[3].GetProperty("day").GetProperty("avgtemp_c").ToString(),
                                day3_max_c = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[3].GetProperty("day").GetProperty("maxtemp_c").ToString(),
                                day3_cond_text = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[3].GetProperty("day").GetProperty("condition").GetProperty("text").GetString(),
                                day3_cond_icon = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[3].GetProperty("day").GetProperty("condition").GetProperty("icon").GetString(),

                                day4_weekday = DateTime.Parse(weatherResponse.GetProperty("forecast").GetProperty("forecastday")[4].GetProperty("date").GetString()).DayOfWeek.ToString(),
                                day4_min_c = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[4].GetProperty("day").GetProperty("mintemp_c").ToString(),
                                day4_avg_c = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[4].GetProperty("day").GetProperty("avgtemp_c").ToString(),
                                day4_max_c = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[4].GetProperty("day").GetProperty("maxtemp_c").ToString(),
                                day4_cond_text = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[4].GetProperty("day").GetProperty("condition").GetProperty("text").GetString(),
                                day4_cond_icon = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[4].GetProperty("day").GetProperty("condition").GetProperty("icon").GetString(),

                                day5_weekday = DateTime.Parse(weatherResponse.GetProperty("forecast").GetProperty("forecastday")[5].GetProperty("date").GetString()).DayOfWeek.ToString(),
                                day5_min_c = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[5].GetProperty("day").GetProperty("mintemp_c").ToString(),
                                day5_avg_c = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[5].GetProperty("day").GetProperty("avgtemp_c").ToString(),
                                day5_max_c = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[5].GetProperty("day").GetProperty("maxtemp_c").ToString(),
                                day5_cond_text = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[5].GetProperty("day").GetProperty("condition").GetProperty("text").GetString(),
                                day5_cond_icon = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[5].GetProperty("day").GetProperty("condition").GetProperty("icon").GetString(),

                                day6_weekday = DateTime.Parse(weatherResponse.GetProperty("forecast").GetProperty("forecastday")[6].GetProperty("date").GetString()).DayOfWeek.ToString(),
                                day6_min_c = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[6].GetProperty("day").GetProperty("mintemp_c").ToString(),
                                day6_avg_c = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[6].GetProperty("day").GetProperty("avgtemp_c").ToString(),
                                day6_max_c = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[6].GetProperty("day").GetProperty("maxtemp_c").ToString(),
                                day6_cond_text = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[6].GetProperty("day").GetProperty("condition").GetProperty("text").GetString(),
                                day6_cond_icon = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[6].GetProperty("day").GetProperty("condition").GetProperty("icon").GetString(),

                                day7_weekday = DateTime.Parse(weatherResponse.GetProperty("forecast").GetProperty("forecastday")[7].GetProperty("date").GetString()).DayOfWeek.ToString(),
                                day7_min_c = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[7].GetProperty("day").GetProperty("mintemp_c").ToString(),
                                day7_avg_c = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[7].GetProperty("day").GetProperty("avgtemp_c").ToString(),
                                day7_max_c = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[7].GetProperty("day").GetProperty("maxtemp_c").ToString(),
                                day7_cond_text = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[7].GetProperty("day").GetProperty("condition").GetProperty("text").GetString(),
                                day7_cond_icon = weatherResponse.GetProperty("forecast").GetProperty("forecastday")[7].GetProperty("day").GetProperty("condition").GetProperty("icon").GetString(),

                            }) ;
                            
                        }
                        catch
                        {

                        }
                    }
                }
            }
            
            return citiesList;
        }

        private async Task<string> FetchWeatherData(string cityName)
        {
            string apiURL = "http://api.weatherapi.com/v1/forecast.json?key=9804259f17b34b729aa213344232412&q=" + cityName + "&days=8";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiURL);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new HttpRequestException($"Error: {response.StatusCode}");
                }
            }
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = _userManager.GetUserAsync(HttpContext.User).Result;

            
            ViewBag.Logger = _logger;
            ViewBag.CurrentUser = currentUser;
            //TODO requests to openweather
            return View(await InitCitiesList(currentUser));//TODO pass openweather data
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public int AddCityToUser(string cityName)
        {
            var currentUser = _userManager.GetUserAsync(HttpContext.User).Result;
            if (currentUser != null)
            {
                currentUser.Cities += cityName + '.';
                _userManager.UpdateAsync(currentUser);
                return 0;
            }
            else
            {
                return 1;
            }
        }
        public int ClearUserCities()
        {
            var currentUser = _userManager.GetUserAsync(HttpContext.User).Result;
            if (currentUser != null)
            {
                currentUser.Cities = null;
                _userManager.UpdateAsync(currentUser);
                return 0;
            }
            else
            {
                return 1;
            }
        }

    }
}