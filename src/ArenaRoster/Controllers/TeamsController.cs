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

        public IActionResult Index()
        {
            return View(_db.Teams.ToList());
        }

        public IActionResult Create()
        {
            Debug.WriteLine("test");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            Player userPlayer = _db.Players.FirstOrDefault(p => p.AppUserId == user.Id);
            Team newTeam = new Team() { Name = name };
            _db.Teams.Add(newTeam);
            _db.SaveChanges();
            PlayerTeam newPlayerTeam = new PlayerTeam() { Player = userPlayer, Team = newTeam };
            _db.PlayersTeams.Add(newPlayerTeam);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            Player player = _db.Players.FirstOrDefault(p => p.AppUserId == user.Id);
            Team team = _db.Teams.FirstOrDefault(t => t.Id == id);
            List<PlayerTeam> roster = _db.PlayersTeams.Include(pt => pt.Player).Where(pt => pt.Team == team).ToList();
            ViewBag.Roster = new List<Player>() { };
            foreach(PlayerTeam playerEntry in roster)
            {
                ViewBag.Roster.Add(playerEntry.Player);
            }
            bool PlayerOnTeam = false;
            foreach(Player teammate in ViewBag.Roster)
            {
                if (teammate.Id == player.Id)
                {
                    PlayerOnTeam = true;
                }
            }
            if (PlayerOnTeam)
            {
                return View(team);
            }
            else
            {
                return RedirectToAction("NotAMember");
            }
        }

        public IActionResult NotAMember()
        {
            return View();
        }
    }
}
