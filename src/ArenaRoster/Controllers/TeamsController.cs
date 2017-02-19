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
            return RedirectToAction("Index");
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
            ViewBag.Roster = _db.PlayersTeams
                .Include(pt => pt.Team)
                .Where(pt => pt.Team == team)
                .Select(pt => pt.AppUser)
                .ToList();
            List<Game> Schedule = _db.Games
                .Include(g => g.AvailablePlayers)
                .Where(g => g.Team == team)
                .ToList();
            return View(Schedule);
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
