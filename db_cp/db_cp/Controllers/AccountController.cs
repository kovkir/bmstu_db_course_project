using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using db_cp.ViewModels;
using db_cp.Interfaces;
using db_cp.Mocks;
using db_cp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using db_cp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;

namespace db_cp.Controllers 
{
    public class AccountController : Controller
    {
        //static private IUserRepository userRepository = new UserMock();
        //private IUserService userService = new UserService(userRepository);

        private readonly IConfiguration configuration;

        IUserService userService;
        ISquadService squadService;

        public AccountController(IConfiguration configuration,
                                 IUserService userService,
                                 ISquadService squadService)
        {
            this.configuration = configuration;
            this.userService = userService;
            this.squadService = squadService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.Title = "Register";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            ViewBag.Title = "Register";

            if (ModelState.IsValid)
            {
                try
                {
                    User user = new User
                    {
                        Login = model.Login,
                        Password = model.Password,
                        Permission = "user"
                    };

                    userService.Add(user);

                    Squad squad = new Squad
                    {
                        CoachId = 0,
                        Name = model.NameMySquad,
                        Rating = 0
                    };

                    squadService.Add(squad);

                    await Authenticate(user);
                    ChangeConnection(user.Permission);

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    ModelState.AddModelError("", ex.Message);
                }
            }
            else
                ModelState.AddModelError("", "Некорректные данные");

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.Title = "Login";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        { 
            ViewBag.Title = "Login";

            if (ModelState.IsValid)
            {
                User user = userService.GetByLogin(model.Login);

                if (user != null && user.Password == model.Password)
                {
                    await Authenticate(user);
                    ChangeConnection(user.Permission);

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            else
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");

            return View(model);
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Permission)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ChangeConnection("guest");

            return RedirectToAction("Index", "Home");
        }

        private void ChangeConnection(string permission)
        {
            if (permission == "user")
                configuration["DatabaseConnection"] = configuration.GetConnectionString("userConnection");
            else if (permission == "admin")
                configuration["DatabaseConnection"] = configuration.GetConnectionString("adminConnection");
            else
                configuration["DatabaseConnection"] = configuration.GetConnectionString("guestConnection");

            Console.WriteLine(permission);
            Console.WriteLine(configuration["DatabaseConnection"]);
        }
    }
}
