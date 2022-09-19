using System;
using System.Collections.Generic;
using db_cp.Models;

namespace db_cp.ViewModels
{
    public enum IsUpdata
    {
        PlayerIsAdded,
        PlayerIsDeleted,

        CoachIsAdded,
        CoachIsDeleted,

        IsNotUpdate
    }

    public class MySquadViewModel
    {
        public Squad mySquad { get; set; }
        public Coach myCoach { get; set; }
        public IEnumerable<Player> myPlayers { get; set; }
        public IEnumerable<Club> clubs { get; set; }

        public Player player { get; set; }
        public Coach coach { get; set; }

        public IsUpdata _isUpdate { get; set; }
    }
}
