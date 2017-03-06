using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ArenaRoster.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        public virtual ApplicationUser TeamManager { get; set; }
        public virtual List<PlayerTeam> Roster { get; set; }
        public virtual List<Game> Schedule { get; set; }
        public string Name { get; set; }
    }
}
