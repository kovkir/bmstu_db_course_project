using System;
using System.ComponentModel.DataAnnotations;

namespace db_cp.Models
{
    public class Coach
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public uint WorkExperience { get; set; }
    }
}
