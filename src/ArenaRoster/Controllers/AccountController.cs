using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ArenaRoster.Models;
using Microsoft.AspNetCore.Identity;
using ArenaRoster.ViewModels;
using Microsoft.EntityFrameworkCore;
using MailKit;
using Microsoft.AspNetCore.Http;
using System.IO;

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
            return View(user);
        }

        public async Task<IActionResult> GetTeams()
        {
            List<Team> teams = new List<Team> { };
            ApplicationUser user = await _userManager.GetUserAsync(User);
            ViewBag.User = user;
            if (user != null)
            {
                teams = _db.PlayersTeams.Include(pt => pt.Team)
                    .Where(pt => pt.AppUser == user)
                    .Select(pt => pt.Team)
                    .ToList();
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

        public async Task<IActionResult> Edit()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUser user, IFormFile Image)
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
            thisUser.NormalizedEmail = thisUser.NormalizedUserName = user.Email.ToUpper();
            _db.Entry(thisUser).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
