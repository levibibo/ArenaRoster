using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecTeam.Models;
using Microsoft.AspNetCore.Identity;
using RecTeam.ViewModels;
using Microsoft.EntityFrameworkCore;
using MailKit;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;

namespace RecTeam.Controllers
{
    public class AccountController : Controller
    {
        private readonly RecTeamDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RecTeamDbContext db)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            return View(user);
        }

        public async Task<IActionResult> GetTeams()
        {
            List<Team> teams = new List<Team> { };
            ApplicationUser user = await _userManager.GetUserAsync(User);
            ViewBag.User = user;
            if (user != null)
            {
                teams = _db.Teams.Include(t => t.Roster)
                        .ThenInclude(r => r.AppUser)
                    .Include(t => t.TeamManager)
                    .ToList();
            }
            foreach (Team team in teams.ToList())
            {
                bool playerOnTeam = false;
                foreach (PlayerTeam playerTeam in team.Roster)
                {
                    if (playerTeam.AppUser.Id == user.Id)
                    {
                        playerOnTeam = true;
                    }
                }
                if (!playerOnTeam)
                {
                    teams.Remove(team);
                }
            }
            return View(teams);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email, Name = model.Name };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                Microsoft.AspNetCore.Identity.SignInResult loginResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
                return RedirectToAction("Edit");
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

        [Authorize]
        public async Task<IActionResult> Edit()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUser user, IFormFile Image, string positionId)
        {
            //REFACTOR
            ApplicationUser thisUser = await _userManager.GetUserAsync(User);
            if (Image != null)
            {
                using (Stream filestream = Image.OpenReadStream())
                using (MemoryStream ms = new MemoryStream())
                {
                    filestream.CopyTo(ms);
                    thisUser.Image = ms.ToArray();
                }
            }
            thisUser.Name = user.Name;
            thisUser.Email = user.Email;
            thisUser.PhoneNumber = user.PhoneNumber;
            thisUser.Position = user.Position;
            thisUser.NormalizedEmail = thisUser.NormalizedUserName = user.Email.ToUpper();
            _db.Entry(thisUser).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            IdentityResult result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
