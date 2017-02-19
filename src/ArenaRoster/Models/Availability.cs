using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArenaRoster.Models
{
    public class Availability
    {
        [Key]
        public int Id { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Game Game { get; set; }
        public bool Available { get; set; }
    }
}
