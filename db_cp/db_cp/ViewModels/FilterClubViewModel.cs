using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using db_cp.Models;

namespace db_cp.ViewModels
{
    public class FilterClubViewModel
    {
        public string name { get; set; }
        public string country { get; set; }

        public uint? minFoundationDate { get; set; }
        public uint? maxFoundationDate { get; set; }
    }
}
