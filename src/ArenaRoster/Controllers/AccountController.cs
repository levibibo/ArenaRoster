using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ArenaRoster.Models;
using Microsoft.AspNetCore.Identity;
using ArenaRoster.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ArenaRoster.Controllers
{
    public class AccountController : Controller
    {
        private readonly ArenaRosterDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ArenaRosterDbContext db)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            Player player = _db.Players.FirstOrDefault(p => p.AppUserId == user.Id);
            ViewBag.Player = player;
            List<PlayerTeam> teams = _db.PlayersTeams.Include(pt => pt.Team)
                .Where(pt => pt.PlayerId == player.Id)
                .OrderBy(pt => pt.Team.Name)
                .ToList();
            foreach(PlayerTeam entry in teams)
            {
                Debug.WriteLine(entry.Team.Name);
            }
            ViewBag.Teams = new List<Team>() { };
            foreach(PlayerTeam teamEntry in teams)
            {
                Debug.WriteLine(teamEntry.Team.Name);
                ViewBag.Teams.Add(teamEntry.Team);
            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                Player newPlayer = new Player() { Name = model.Name, AppUserId = user.Id };
                _db.Players.Add(newPlayer);
                _db.SaveChanges();
                Microsoft.AspNetCore.Identity.SignInResult loginResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
