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

        private static string[] Alphabet = new string[26]
        {
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m",
        "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
        };

        public static string GeneratePassword()
        {
            Random random = new Random();
            string numbers = random.Next(0, 9999).ToString();
            while(numbers.Length < 4)
            {
                numbers = "0" + numbers;
            }
            string password = Alphabet[random.Next(0, 25)].ToUpper() +
                Alphabet[random.Next(0, 25)] +
                Alphabet[random.Next(0, 25)] +
                Alphabet[random.Next(0, 25)] +
                numbers;
            return password;
        }
    }
}
