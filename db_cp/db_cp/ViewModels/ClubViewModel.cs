using System;
using System.Collections.Generic;
using db_cp.Models;

namespace db_cp.ViewModels
{
    public class ClubViewModel
    {
        public IEnumerable<Club> clubs { get; set; }

        public FilterClubViewModel filterClubViewModel { get; set; }
    }
}
