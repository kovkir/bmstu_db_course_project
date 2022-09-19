using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace db_cp.Models
{
    public class Squad
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Coach")]
        public int CoachId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public uint Rating { get; set; }
    }
}
