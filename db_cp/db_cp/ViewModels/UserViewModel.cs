using System;
using System.Collections.Generic;
using db_cp.Models;

namespace db_cp.ViewModels
{
    public class UserViewModel
    {
        public IEnumerable<User> users { get; set; }
        public IEnumerable<Squad> squads { get; set; }
    }
}
