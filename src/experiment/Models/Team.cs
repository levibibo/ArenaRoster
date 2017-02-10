using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ArenaRoster.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public List<UserTeam> UserTeams { get; set; } 
    }
}
