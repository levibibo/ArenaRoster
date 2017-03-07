using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RecTeam.Models
{
    [Table("Availabilities")]
    public class Availability
    {
        [Key]
        public int Id { get; set; }
        public virtual ApplicationUser AppUser { get; set; }
        public virtual Game Game { get; set; }
        public bool Available { get; set; }
    }
}
