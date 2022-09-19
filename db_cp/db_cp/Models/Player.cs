using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace db_cp.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Club")]
        public int ClubId { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public uint Rating { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public uint Price { get; set; }
    }
}
