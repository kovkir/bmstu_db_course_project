using System;
using System.Collections.Generic;
using db_cp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace db_cp.ViewModels
{
    public enum IsAction
    {
        SearchPlayers,
        SearchCoaches,
        SearchClubs,
        AddPlayer,
        DeletePlayer
    }

    public class HomeViewModel
    {
        public FilterPlayerViewModel filterPlayerViewModel { get; set; }
        public FilterCoachViewModel filterCoachViewModel { get; set; }
        public FilterClubViewModel filterClubViewModel { get; set; }

        public AddPlayerViewModel addPlayerViewModel { get; set; }
        public DeletePlayerViewModel deletePlayerViewModel { get; set; }
    }
}
