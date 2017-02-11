using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ArenaRoster.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace ArenaRoster.Controllers
{
    public class TeamsController : Controller
    {
        private ArenaRosterDbContext db = new ArenaRosterDbContext();

        public IActionResult Index()
        {
            return View(db.Teams.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Team newTeam)
        {
            db.Teams.Add(newTeam);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            ViewBag.Users = new SelectList(db.Users, "UserId", "Name");
            return View(db.Teams
                .Include(p => p.UserTeams)
                .ThenInclude(bp => bp.User)
                .FirstOrDefault(team => team.TeamId == id));
        }
        [HttpPost]
        public IActionResult Details(int id, User selectedUser)
        {
            Debug.WriteLine(selectedUser.Name);
            //UserTeam newUserTeam = new UserTeam(selectedUser.UserId, id);
            //db.UserTeam.Add(newUserTeam);
            //db.SaveChanges();
            return RedirectToAction("Details");
        }
    }
}
