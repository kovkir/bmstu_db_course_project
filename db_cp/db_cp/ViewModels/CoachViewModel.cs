using System;
using System.Collections.Generic;
using db_cp.Models;

namespace db_cp.ViewModels
{
    public class CoachViewModel
    {
        public IEnumerable<Coach> coaches { get; set; }
        public int myCoachId { get; set; }

        public FilterCoachViewModel filterCoachViewModel { get; set; }
    }
}
