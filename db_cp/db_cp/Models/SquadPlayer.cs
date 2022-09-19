using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace db_cp.Models
{
    public class SquadPlayer
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Squad")]
        public int SquadId { get; set; }

        [ForeignKey("Player")]
        public int PlayerId { get; set; }
    }
}
