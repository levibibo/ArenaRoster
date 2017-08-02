using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecTeam.Models
{
    public class Position
    {
        [Key]
        public int Id { get; set; }
        public List<ApplicationUser> AppUsers { get; set; }
        public string Name { get; set; }
    }
}
