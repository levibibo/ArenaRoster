using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecTeam.Models
{
    public class UnreadMessage
    {
        [Key]
        public int Id { get; set; }
        public virtual ChatMessage Message { get; set; }
        public virtual Team Team { get; set; }
        public virtual ApplicationUser AppUser { get; set; }
    }
}
