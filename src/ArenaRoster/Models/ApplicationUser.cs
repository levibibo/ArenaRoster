using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ArenaRoster.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Position Position { get; set; }
        public List<Game> Games { get; set; }
        public List<PlayerTeam> Teams { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
    }
}
