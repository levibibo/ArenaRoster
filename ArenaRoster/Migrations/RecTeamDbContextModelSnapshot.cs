using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using RecTeam.Models;

namespace ArenaRoster.Migrations
{
    [DbContext(typeof(RecTeamDbContext))]
    partial class RecTeamDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("RecTeam.Models.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<byte[]>("Image");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("Position");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("RecTeam.Models.Availability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AppUserId");

                    b.Property<bool>("Available");

                    b.Property<int?>("GameId");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("GameId");

                    b.ToTable("Availabilities");
                });

            modelBuilder.Entity("RecTeam.Models.ChatMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AppUserId");

                    b.Property<string>("Message");

                    b.Property<DateTime>("PostDateTime");

                    b.Property<int?>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("TeamId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("RecTeam.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ApplicationUserId");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Location");

                    b.Property<int?>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("TeamId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("RecTeam.Models.PlayerTeam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AppUserId");

                    b.Property<int>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("TeamId");

                    b.ToTable("PlayersTeams");
                });

            modelBuilder.Entity("RecTeam.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int?>("TeamManagerId");

                    b.HasKey("Id");

                    b.HasIndex("TeamManagerId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("RecTeam.Models.UnreadMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AppUserId");

                    b.Property<int?>("MessageId");

                    b.Property<int?>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("MessageId");

                    b.HasIndex("TeamId");

                    b.ToTable("UnreadMessages");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole<int>")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("RecTeam.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("RecTeam.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole<int>")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RecTeam.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RecTeam.Models.Availability", b =>
                {
                    b.HasOne("RecTeam.Models.ApplicationUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId");

                    b.HasOne("RecTeam.Models.Game", "Game")
                        .WithMany("AvailablePlayers")
                        .HasForeignKey("GameId");
                });

            modelBuilder.Entity("RecTeam.Models.ChatMessage", b =>
                {
                    b.HasOne("RecTeam.Models.ApplicationUser", "AppUser")
                        .WithMany("Messages")
                        .HasForeignKey("AppUserId");

                    b.HasOne("RecTeam.Models.Team", "Team")
                        .WithMany("Messages")
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("RecTeam.Models.Game", b =>
                {
                    b.HasOne("RecTeam.Models.ApplicationUser")
                        .WithMany("Games")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("RecTeam.Models.Team", "Team")
                        .WithMany("Schedule")
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("RecTeam.Models.PlayerTeam", b =>
                {
                    b.HasOne("RecTeam.Models.ApplicationUser", "AppUser")
                        .WithMany("Teams")
                        .HasForeignKey("AppUserId");

                    b.HasOne("RecTeam.Models.Team", "Team")
                        .WithMany("Roster")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RecTeam.Models.Team", b =>
                {
                    b.HasOne("RecTeam.Models.ApplicationUser", "TeamManager")
                        .WithMany()
                        .HasForeignKey("TeamManagerId");
                });

            modelBuilder.Entity("RecTeam.Models.UnreadMessage", b =>
                {
                    b.HasOne("RecTeam.Models.ApplicationUser", "AppUser")
                        .WithMany("UnreadMessages")
                        .HasForeignKey("AppUserId");

                    b.HasOne("RecTeam.Models.ChatMessage", "Message")
                        .WithMany()
                        .HasForeignKey("MessageId");

                    b.HasOne("RecTeam.Models.Team", "Team")
                        .WithMany("UnreadMessages")
                        .HasForeignKey("TeamId");
                });
        }
    }
}
