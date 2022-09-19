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
using Microsoft.EntityFrameworkCore;

namespace db_cp.Controllers
{
    public class ClubController : Controller
    {
        //static private IClubRepository clubRepository = new ClubMock();
        //private IClubService clubService = new ClubService(clubRepository);

        IClubService clubService;
        private readonly AppDBContext _appDBContext;

        public ClubController(IClubService clubService, AppDBContext appDBContext)
        {
            this.clubService = clubService;
            this._appDBContext = appDBContext;
        }

        public IActionResult GetAllClubs(ClubSortState sortOrder = ClubSortState.IdAsc,
                                         string name = null, string country = null,
                                         uint minFoundationDate = 0, uint maxFoundationDate = 0)
        {
            ViewBag.Title = "Clubs";

            ViewData["IdSort"]             = sortOrder == ClubSortState.IdAsc             ? ClubSortState.IdDesc             : ClubSortState.IdAsc;
            ViewData["NameSort"]           = sortOrder == ClubSortState.NameAsc           ? ClubSortState.NameDesc           : ClubSortState.NameAsc;
            ViewData["CountrySort"]        = sortOrder == ClubSortState.CountryAsc        ? ClubSortState.CountryDesc        : ClubSortState.CountryAsc;
            ViewData["FoundationDateSort"] = sortOrder == ClubSortState.FoundationDateAsc ? ClubSortState.FoundationDateDesc : ClubSortState.FoundationDateAsc;

            //Реализация через C#
            //IEnumerable<Club> clubs = clubService.GetByParameters(name, country, minFoundationDate, maxFoundationDate);

            //Реализация через функцию БД
            IEnumerable<Club> clubs = _appDBContext.Club
                .FromSqlInterpolated($"SELECT * FROM getClubsByParameters({name}, {country}, {minFoundationDate}, {maxFoundationDate})");

            ClubViewModel allClubs = new ClubViewModel
            {
                clubs = clubService.GetSortClubsByOrder(clubs, sortOrder),

                filterClubViewModel = new FilterClubViewModel
                {
                    name = name,
                    country = country,
                    minFoundationDate = minFoundationDate,
                    maxFoundationDate = maxFoundationDate
                }
            };

            return View(allClubs);
        }
    }
}
