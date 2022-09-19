using System;
using System.ComponentModel.DataAnnotations;

namespace db_cp.Models
{
    public class Club
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public uint FoundationDate { get; set; }
    }
}
