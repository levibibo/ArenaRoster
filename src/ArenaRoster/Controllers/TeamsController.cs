//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using ArenaRoster.Models;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using System.Diagnostics;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Identity;

//namespace ArenaRoster.Controllers
//{
//    public class TeamsController : Controller
//    {
//        private readonly ArenaRosterDbContext _db;
//        private readonly UserManager<ApplicationUser> _userManager;

//        public TeamsController(UserManager<ApplicationUser> userManager, ArenaRosterDbContext db)
//        {
//            _userManager = userManager;
//            _db = db;
//        }

//        public IActionResult Index()
//        {
//            return View(_db.Teams.ToList());
//        }

//        public IActionResult Create()
//        {
//            return View();
//        }
//        [HttpPost]
//        public IActionResult Create(Team newTeam)
//        {
//            _db.Teams.Add(newTeam);
//            _db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        public IActionResult Details(int id)
//        {
//            ViewBag.Players = new SelectList(_db.Players, "PlayerId", "Name");
//            return View(_db.Teams
//                .Include(p => p.PlayerTeams)
//                .ThenInclude(bp => bp.Player)
//                .FirstOrDefault(team => team.TeamId == id));
//        }
//        [HttpPost]
//        public IActionResult Details(int id, Player selectedPlayer)
//        {
//            Debug.WriteLine(selectedPlayer.Name);
//            //PlayerTeam newPlayerTeam = new PlayerTeam(selectedPlayer.PlayerId, id);
//            //db.PlayerTeam.Add(newPlayerTeam);
//            //db.SaveChanges();
//            return RedirectToAction("Details");
//        }
//    }
//}
