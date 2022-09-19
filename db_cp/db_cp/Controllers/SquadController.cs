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
    public class SquadController : Controller
    {
        //static private ISquadRepository squadRepository = new SquadMock();
        //private ICoachService coachService = new CoachService(coachRepository);

        private ISquadService squadService;
        private ICoachService coachService;

        public SquadController(ISquadService squadService, ICoachService coachService)
        {
            this.squadService = squadService;
            this.coachService = coachService;
        }

        public IActionResult GetAllSquads(SquadSortState sortOrder = SquadSortState.IdAsc)
        {
            ViewBag.Title = "Squads";

            ViewData["IdSort"]           = sortOrder == SquadSortState.IdAsc           ? SquadSortState.IdDesc           : SquadSortState.IdAsc;
            ViewData["CoachSurnameSort"] = sortOrder == SquadSortState.CoachSurnameAsc ? SquadSortState.CoachSurnameDesc : SquadSortState.CoachSurnameAsc;
            ViewData["NameSort"]         = sortOrder == SquadSortState.NameAsc         ? SquadSortState.NameDesc         : SquadSortState.NameAsc;
            ViewData["RatingSort"]       = sortOrder == SquadSortState.RatingAsc       ? SquadSortState.RatingDesc       : SquadSortState.RatingAsc;

            var allSquads = new SquadViewModel
            {
                coaches = coachService.GetAll(),
                squads = squadService.GetSortSquadsByOrder(sortOrder)
            };

            return View(allSquads);
        }
    }
}
