using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecTeam.Models
{
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime PostDateTime { get; set; }
        public virtual Team Team { get; set; }
        public virtual ApplicationUser AppUser { get; set; }
    }
}
