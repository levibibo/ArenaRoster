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

        public IActionResult Details(int id)
        {
            return View(_db.Teams.FirstOrDefault(team => team.Id == id));
        }
        //[HttpPost]
        //public async Task<IActionResult> Details(string name)
        //{
        //    Team newTeam = new Team(name);
        //    newTeam.Users.Add(await _userManager.FindByIdAsync(User.Claims.));
        //    _db.Teams.Add(newTeam);
        //    _db.SaveChanges();
        //    return RedirectToAction("Details");
        //}
    }
}
