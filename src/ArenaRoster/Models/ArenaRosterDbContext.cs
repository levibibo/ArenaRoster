﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ArenaRoster.Models
{
    public class ArenaRosterDbContext : IdentityDbContext<ApplicationUser>
    {
        public ArenaRosterDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<PlayerTeam> PlayersTeams { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<Position> Positions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ArenaRosterDb;integrated security=True");
        }
    }
}
