using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RecTeam.Models
{
    public class RecTeamDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public RecTeamDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<PlayerTeam> PlayersTeams { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<ChatMessage> Messages { get; set; }
        public DbSet<UnreadMessage> UnreadMessages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer($@"Data Source=tcp:recteamdbserver.database.windows.net,1433;Initial Catalog=RecTeamDb;User Id={EnvironmentVariables.DbUserId};Password={EnvironmentVariables.DbPassword}");
        }
    }
}
