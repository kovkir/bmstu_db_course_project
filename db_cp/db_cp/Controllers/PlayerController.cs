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
    public class PlayerController : Controller
    {
        //static private IPlayerRepository playerRepository = new PlayerMock();
        //static private IClubRepository clubRepository = new ClubMock();

        //private IPlayerService playerService = new PlayerService(playerRepository);
        //private IClubService clubService = new ClubService(clubRepository);

        private IPlayerService playerService;
        private IClubService clubService;
        private ISquadService squadService;
        private IUserService userService;

        public PlayerController(IPlayerService playerService,
                                IClubService   clubService,
                                ISquadService  squadService)
        {
            this.playerService = playerService;
            this.clubService   = clubService;
            this.squadService  = squadService;
        }

        public IActionResult GetAllPlayers(PlayerSortState sortOrder = PlayerSortState.RatingDesc,
                                           string surname = null, string country = null, string clubName = null,
                                           uint minPrice = 0, uint maxPrice = 0, uint minRating = 0, uint maxRating = 0,
                                           int squadId = 0)
        {
            ViewBag.Title = "Players";

            ViewData["IdSort"]       = sortOrder == PlayerSortState.IdAsc       ? PlayerSortState.IdDesc       : PlayerSortState.IdAsc;
            ViewData["SurnameSort"]  = sortOrder == PlayerSortState.SurnameAsc  ? PlayerSortState.SurnameDesc  : PlayerSortState.SurnameAsc;
            ViewData["RatingSort"]   = sortOrder == PlayerSortState.RatingDesc  ? PlayerSortState.RatingAsc    : PlayerSortState.RatingDesc;
            ViewData["CountrySort"]  = sortOrder == PlayerSortState.CountryAsc  ? PlayerSortState.CountryDesc  : PlayerSortState.CountryAsc;
            ViewData["ClubNameSort"] = sortOrder == PlayerSortState.ClubNameAsc ? PlayerSortState.ClubNameDesc : PlayerSortState.ClubNameAsc;
            ViewData["PriceSort"]    = sortOrder == PlayerSortState.PriceDesc   ? PlayerSortState.PriceAsc     : PlayerSortState.PriceDesc;

            IEnumerable<Player> players = playerService.GetByParameters(surname, country, clubName, minPrice,
                                                                        maxPrice, minRating, maxRating, squadId);

            PlayerViewModel allPlayers = new PlayerViewModel
            {
                players = playerService.GetSortPlayersByOrder(players, sortOrder),
                myPlayers = squadService.GetMyPlayersByUserLogin(User.Identity.Name),
                clubs = clubService.GetAll(),

                filterPlayerViewModel = new FilterPlayerViewModel
                {
                    surname = surname,
                    country = country,
                    clubName = clubName,
                    minPrice = minPrice,
                    maxPrice = maxPrice,
                    minRating = minRating,
                    maxRating = maxRating,
                    squadId = squadId
                }
            };

            return View(allPlayers);
        }
    }
}
