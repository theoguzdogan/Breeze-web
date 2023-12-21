using LoginSolo.Entities;
using LoginSolo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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

  


        public IActionResult Index()
        {
            var currentUser = _userManager.GetUserAsync(HttpContext.User).Result;

            
            ViewBag.Logger = _logger;
            ViewBag.CurrentUser = currentUser;
            //TODO requests to openweather
            return View();//TODO pass openweather data
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