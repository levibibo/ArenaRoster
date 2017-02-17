﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArenaRoster.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public string AppUserId { get; set; }
        public Position Position { get; set; }
        public ApplicationUser AppUser { get; set; }
        public List<PlayerTeam> Teams { get; set; }
    }
}
