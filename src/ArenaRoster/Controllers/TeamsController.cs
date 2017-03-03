using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ArenaRoster.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace ArenaRoster.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ArenaRosterDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public TeamsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ArenaRosterDbContext db)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            return View(_db.Teams.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Team newTeam)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            _db.Teams.Add(newTeam);
            _db.SaveChanges();
            PlayerTeam newPlayerTeam = new PlayerTeam() { AppUser = user, Team = newTeam };
            _db.PlayersTeams.Add(newPlayerTeam);
            _db.SaveChanges();
            return RedirectToAction("Details", new { id = newTeam.Id });
        }

        public IActionResult AddPlayer(int id)
        {
            ViewBag.Team = _db.Teams.FirstOrDefault(t => t.Id == id);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddPlayer(int id, string email)
        {
            Team team = _db.Teams.FirstOrDefault(t => t.Id == id);
            ApplicationUser user = _db.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                user = new ApplicationUser() { Email = email, UserName = email };
                IdentityResult result = await _userManager.CreateAsync(user, "Password");
            }
            PlayerTeam newTeammate = new PlayerTeam() { };
            newTeammate.AppUser = user;
            newTeammate.Team = team;
            _db.PlayersTeams.Add(newTeammate);
            _db.SaveChanges();
            return RedirectToAction("Details", new { id = team.Id });
        }

        public async Task<IActionResult> Details(int id)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            Team team = _db.Teams.FirstOrDefault(t => t.Id == id);
            ViewBag.Roster = _db.PlayersTeams.Include(pt => pt.AppUser)
                .Where(pt => pt.Team == team)
                .Select(pt => pt.AppUser)
                .ToList();
            foreach(ApplicationUser teammate in ViewBag.Roster)
            {
                if (teammate == user)
                {
                    return View(team);
                }
            }
            return RedirectToAction("NotAMember");
        }

        public IActionResult Schedule(int id)
        {
            Team team = _db.Teams.FirstOrDefault(t => t.Id == id);
            ViewBag.Team = team;
            List<Game> Schedule = _db.Games
                .Include(g => g.AvailablePlayers)
                    .ThenInclude(a => a.AppUser)
                .Where(g => g.Team == team)
                .OrderBy(g => g.Date)
                .ToList();
            return View(Schedule);
        }

        public IActionResult AddGame(int id)
        {
            Team team = _db.Teams.FirstOrDefault(t => t.Id == id);
            ViewBag.Team = team;
            return View();
        }

        [HttpPost]
        public IActionResult AddGame(int teamId, Game game, string date, string time)
        {
            //Date
            string[] dateArray = date.Split('-');
            string[] timeArray = time.Split(':');
            string dateTime = $"{date} {time}";
            DateTime newDate = Convert.ToDateTime(dateTime);
            Debug.WriteLine(newDate.ToString());

            //Team
            Team team = _db.Teams.FirstOrDefault(t => t.Id == teamId);
            ViewBag.Team = team;

            //Game
            game.Date = newDate;
            game.Team = team;
            game.Id = 0; //Game Id is being set to 1 and causing EF to try to insert an ID into the Identity field
            _db.Games.Add(game);
            _db.SaveChanges();

            //Roster
            List<ApplicationUser> roster = _db.PlayersTeams
                .Include(pt => pt.AppUser)
                .Where(pt => pt.TeamId == team.Id)
                .Select(pt => pt.AppUser)
                .ToList();

            //Availability
            foreach(ApplicationUser teammate in roster)
            {
                Availability newTeammate = new Availability() { };
                newTeammate.AppUser = teammate;
                newTeammate.Game = game;
                newTeammate.Available = true;
                _db.Availabilities.Add(newTeammate);
                _db.SaveChanges();
            }

            return RedirectToAction("Details", new { id = team.Id });
        }

        public IActionResult Admin(int id)
        {
            Team team = _db.Teams.FirstOrDefault(t => t.Id == id);
            return View(team);
        }

        public IActionResult NotAMember()
        {
            return View();
        }
    }
}
