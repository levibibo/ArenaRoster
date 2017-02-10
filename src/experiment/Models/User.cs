using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace ArenaRoster.Models
{
    public class User
    {
        public User()
        {

        }
        public User(string name, string email, string phone, byte[] picture)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Picture = picture;
        }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte[] Picture { get; set; }
        public List<UserTeam> UserTeams { get; set; }

    }
}
