using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ArenaRoster.Models;

namespace ArenaRoster.Migrations
{
    [DbContext(typeof(ArenaRosterDbContext))]
    [Migration("20170211065702_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ArenaRoster.Models.Team", b =>
                {
                    b.Property<int>("TeamId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("TeamId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("ArenaRoster.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.Property<byte[]>("Picture");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ArenaRoster.Models.UserTeam", b =>
                {
                    b.Property<int>("UserTeamId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("TeamId");

                    b.Property<int>("UserId");

                    b.HasKey("UserTeamId");

                    b.HasIndex("TeamId");

                    b.HasIndex("UserId");

                    b.ToTable("UserTeam");
                });

            modelBuilder.Entity("ArenaRoster.Models.UserTeam", b =>
                {
                    b.HasOne("ArenaRoster.Models.Team", "Team")
                        .WithMany("UserTeams")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ArenaRoster.Models.User", "User")
                        .WithMany("UserTeams")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
