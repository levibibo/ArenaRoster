using System;
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
        //public DbSet<Player> Players { get; set; }
        //public DbSet<Team> Teams { get; set; }
        //public DbSet<PlayerTeam> PlayerTeam { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ArenaRosterDb;integrated security=True");
        }

        //    protected override void OnModelCreating(ModelBuilder modelBuilder)
        //    {
        //        modelBuilder.Entity<PlayerTeam>()
        //            .HasKey(ut => ut.PlayerTeamId);
        //        modelBuilder.Entity<PlayerTeam>()
        //            .HasOne(ut => ut.Player)
        //            .WithMany(u => u.PlayerTeams)
        //            .HasForeignKey(ut => ut.PlayerId);
        //        modelBuilder.Entity<PlayerTeam>()
        //            .HasOne(ut => ut.Team)
        //            .WithMany(t => t.PlayerTeams)
        //            .HasForeignKey(ut => ut.TeamId);
        //    }
    }

    //public class PlayerTeam
    //{
    //    public PlayerTeam()
    //    {

    //    }
    //    public PlayerTeam(int userId, int teamId)
    //    {
    //        PlayerId = userId;
    //        TeamId = teamId;
    //    }

    //    [Key]
    //    public int PlayerTeamId { get; set; }
    //    public int PlayerId { get; set; }
    //    public Player Player { get; set; }
    //    public int TeamId { get; set; }
    //    public Team Team { get; set; }

    //}
}
