using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using db_cp.ViewModels;
using db_cp.Interfaces;
using db_cp.Mocks;
using db_cp.Services;
using Microsoft.AspNetCore.Mvc;
using db_cp.Models;

namespace db_cp.Controllers
{
    public class HomeController : Controller
    {
        private IUserService userService;
        private IPlayerService playerService;
        private IClubService clubService;
        private ISquadService squadService;

        public HomeController(IUserService userService,
                              IPlayerService playerService,
                              IClubService clubService,
                              ISquadService squadService)
        {
            this.userService = userService;
            this.playerService = playerService;
            this.clubService = clubService;
            this.squadService = squadService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Title = "DoStart";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(HomeViewModel model, IsAction isAction = IsAction.SearchPlayers)
        {
            ViewBag.Title = "Find";

            if (ModelState.IsValid)
            {
                if (isAction == IsAction.SearchPlayers)
                {
                    return SearchPlayers(model.filterPlayerViewModel);
                }
                else if (isAction == IsAction.SearchCoaches)
                {
                    return SearchCoaches(model.filterCoachViewModel);
                }
                else if (isAction == IsAction.SearchClubs)
                {
                    return SearchClubs(model.filterClubViewModel);
                }
                else if (isAction == IsAction.AddPlayer)
                {
                    return AddPlayer(model.addPlayerViewModel);
                }
                else if (isAction == IsAction.DeletePlayer)
                {
                    return DeletePlayer(model.deletePlayerViewModel);
                }
            }
            else
                ModelState.AddModelError("", "Некорректные данные");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        private IActionResult SearchPlayers(FilterPlayerViewModel model)
        {
            uint minPrice = checkForNull(model.minPrice);
            uint maxPrice = checkForNull(model.maxPrice);
            uint minRating = checkForNull(model.minRating);
            uint maxRating = checkForNull(model.maxRating);
            int squadId = getSquadId(model.playerSearch);

            return RedirectToAction("GetAllPlayers", "Player",
                new
                {
                    surname = model.surname,
                    country = model.country,
                    clubName = model.clubName,
                    minPrice = minPrice,
                    maxPrice = maxPrice,
                    minRating = minRating,
                    maxRating = maxRating,
                    squadId = squadId
                });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        private IActionResult SearchCoaches(FilterCoachViewModel model)
        {
            uint minWorkExperience = checkForNull(model.minWorkExperience);
            uint maxWorkExperience = checkForNull(model.maxWorkExperience);

            return RedirectToAction("GetAllCoaches", "Coach",
                new
                {
                    surname = model.surname,
                    country = model.country,
                    minWorkExperience = minWorkExperience,
                    maxWorkExperience = maxWorkExperience
                });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        private IActionResult SearchClubs(FilterClubViewModel model)
        {
            uint minFoundationDate = checkForNull(model.minFoundationDate);
            uint maxFoundationDate = checkForNull(model.maxFoundationDate);

            return RedirectToAction("GetAllClubs", "Club",
                new
                {
                    name = model.name,
                    country = model.country,
                    minFoundationDate = minFoundationDate,
                    maxFoundationDate = maxFoundationDate
                });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        private IActionResult AddPlayer(AddPlayerViewModel model)
        {
            try
            {
                Player player = new Player
                {
                    Surname = model.surname,
                    Country = model.country,
                    Rating = model.rating,
                    Price = model.price,
                    ClubId = clubService.GetByName(model.clubName).Id
                };

                playerService.Add(player);

                return RedirectToAction("GetAllPlayers", "Player",
                    new
                    {
                        surname = model.surname,
                        country = model.country,
                        clubName = model.clubName,
                        minPrice = model.price,
                        maxPrice = model.price,
                        minRating = model.rating,
                        maxRating = model.rating,
                        squadId = 0
                    });
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                ModelState.AddModelError("", "Некорректные данные");
            }

            return View(new HomeViewModel { addPlayerViewModel = model });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        private IActionResult DeletePlayer(DeletePlayerViewModel model)
        {
            try
            {
                Player player = playerService.GetByParameters(
                    model.surname, model.country, model.clubName, 0, 0,
                    model.rating, model.rating, 0).First();

                squadService.DeleteAllSquadPlayer(player.Id);
                playerService.Delete(player);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                ModelState.AddModelError("", "Некорректные данные");
            }

            return View(new HomeViewModel { deletePlayerViewModel = model });
        }

        private uint checkForNull(uint? value)
        {
            uint newValue;

            if (value != null)
                newValue = (uint) value;
            else
                newValue = 0;

            return newValue;
        }

        private int getSquadId(string playerSearch)
        {
            int squadId;

            if (playerSearch == "MySquadPlayers")
                squadId = userService.GetByLogin(User.Identity.Name).Id;
            else
                squadId = 0;

            return squadId;
        }
    }
}
