using LoginSolo.Dtos;
using LoginSolo.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LoginSolo.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(AppUserRegisterDto appUserRegisterDto)
        {
            if (ModelState.IsValid)            {
       
                AppUser appUser = new AppUser()
                {
                    UserName = appUserRegisterDto.Name,
                    Email = appUserRegisterDto.Email,                 
                };
                var result = await _userManager.CreateAsync(appUser, appUserRegisterDto.Password);

            }
            return View();
        }
    }
}
