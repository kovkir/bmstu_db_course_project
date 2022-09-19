using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using db_cp.Models;

namespace db_cp.ViewModels
{
    public class PlayerViewModel
    {
        public IEnumerable<Player> players { get; set; }
        public IEnumerable<Player> myPlayers { get; set; }
        public IEnumerable<Club> clubs { get; set; }

        public FilterPlayerViewModel filterPlayerViewModel { get; set; }
    }
}
