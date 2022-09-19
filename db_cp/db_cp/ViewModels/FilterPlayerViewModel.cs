using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using db_cp.Models;

namespace db_cp.ViewModels
{
    public class FilterPlayerViewModel
    {
        public string surname { get; set; }
        public string country { get; set; }
        public string clubName { get; set; }

        public uint? minPrice { get; set; }
        public uint? maxPrice { get; set; }
        public uint? minRating { get; set; }
        public uint? maxRating { get; set; }

        public int squadId { get; set; }
        public string playerSearch { get; set; }
    }
}
