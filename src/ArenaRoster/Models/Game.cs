using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecTeam.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        public virtual Team Team { get; set; }
        public virtual List<Availability> AvailablePlayers { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
    }
}
