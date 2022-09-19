using System;
using System.Collections.Generic;
using db_cp.Models;

namespace db_cp.ViewModels
{
    public class SquadViewModel
    {
        public IEnumerable<Squad> squads { get; set; }
        public IEnumerable<Coach> coaches { get; set; }
    }
}
