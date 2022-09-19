using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using db_cp.Models;

namespace db_cp.ViewModels
{
    public class FilterCoachViewModel
    {
        public string surname { get; set; }
        public string country { get; set; }

        public uint? minWorkExperience { get; set; }
        public uint? maxWorkExperience { get; set; }
    }
}