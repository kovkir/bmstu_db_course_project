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
    public class CoachController : Controller
    {
        //static private ICoachRepository coachRepository = new CoachMock();
        //private ICoachService coachService = new CoachService(coachRepository);

        private ICoachService coachService;
        private ISquadService squadService;

        public CoachController(ICoachService coachService,
                               ISquadService squadService)
        {
            this.coachService = coachService;
            this.squadService = squadService;
        }

        public IActionResult GetAllCoaches(CoachSortState sortOrder = CoachSortState.IdAsc,
                                           string surname = null, string country = null,
                                           uint minWorkExperience = 0, uint maxWorkExperience = 0)
        {
            ViewBag.Title = "Coaches";

            ViewData["IdSort"]             = sortOrder == CoachSortState.IdAsc             ? CoachSortState.IdDesc             : CoachSortState.IdAsc;
            ViewData["SurnameSort"]        = sortOrder == CoachSortState.SurnameAsc        ? CoachSortState.SurnameDesc        : CoachSortState.SurnameAsc;
            ViewData["CountrySort"]        = sortOrder == CoachSortState.CountryAsc        ? CoachSortState.CountryDesc        : CoachSortState.CountryAsc;
            ViewData["WorkExperienceSort"] = sortOrder == CoachSortState.WorkExperienceAsc ? CoachSortState.WorkExperienceDesc : CoachSortState.WorkExperienceAsc;

            IEnumerable<Coach> coaches = coachService.GetByParameters(surname, country, minWorkExperience, maxWorkExperience);

            CoachViewModel allCoaches = new CoachViewModel
            {
                coaches = coachService.GetSortCoachesByOrder(coaches, sortOrder),
                myCoachId = squadService.GetMyCoachIdByUserLogin(User.Identity.Name),

                filterCoachViewModel = new FilterCoachViewModel
                {
                    surname = surname,
                    country = country,
                    minWorkExperience = minWorkExperience,
                    maxWorkExperience = maxWorkExperience
                }
            };

            return View(allCoaches);
        }
    }
}
