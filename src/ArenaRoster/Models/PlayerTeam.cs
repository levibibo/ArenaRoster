using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecTeam.Models
{
    public class PlayerTeam
    {
        [Key]
        public int Id { get; set; }
        public ApplicationUser AppUser { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
