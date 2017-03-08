using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecTeam.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace RecTeam.Controllers
{
    public class TeamsController : Controller
    {
        private readonly RecTeamDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public TeamsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RecTeamDbContext db)
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
            newTeam.TeamManager = user;
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

            //Create new user if the user doesn't exist
            //Randomly generate password and email to registered email address
            //Password is not stored in plain text on database
            if (user == null)
            {
                string password = ApplicationUser.GeneratePassword();
                user = new ApplicationUser() { Email = email, UserName = email, Name = email.Split('@')[0] };
                IdentityResult result = await _userManager.CreateAsync(user, password);
                MailgunApi.SendNewUserEmail(email, team.Name, password);
            }
            else
            {
                MailgunApi.SendNewTeammateEmail(email, team.Name);
            }
            //Add new user to team
            PlayerTeam newTeammate = new PlayerTeam() { };
            newTeammate.AppUser = user;
            newTeammate.Team = team;
            _db.PlayersTeams.Add(newTeammate);
            _db.SaveChanges();

            //Add player as available to all future games
            List<Game> games = _db.Games.Include(g => g.Team).ToList();
            DateTime today = DateTime.Today;
            foreach (Game game in games)
            {
                if (game.Date > today)
                {
                    _db.Availabilities.Add(new Availability()
                    {
                        AppUser = user,
                        Game = game,
                        Available = true
                    });
                }
            }
            return RedirectToAction("Details", new { id = team.Id });
        }

        public IActionResult RemovePlayer(int id)
        {
            Team team = _db.Teams.FirstOrDefault(t => t.Id == id);
            ViewBag.Team = team;
            ViewBag.roster = _db.PlayersTeams.Include(pt => pt.AppUser)
                .Where(pt => pt.Team == team).ToList();
            return View();
        }

        [HttpPost]
        public IActionResult RemovePlayer(int id, int teammateId)
        {
            PlayerTeam player = _db.PlayersTeams.FirstOrDefault(pt => pt.Id == teammateId);
            _db.PlayersTeams.Remove(player);
            _db.SaveChanges();
            return RedirectToAction("Details", new { id = id });
        }

        public async Task<IActionResult> Details(int id)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            Team team = _db.Teams.Include(t => t.TeamManager)
                .FirstOrDefault(t => t.Id == id);
            //if (team == null)
            //{
            //    return RedirectToAction("NotValidTeam");
            //}
            ViewBag.Roster = _db.PlayersTeams.Include(pt => pt.AppUser)
                .Where(pt => pt.Team == team)
                .Select(pt => pt.AppUser)
                .ToList();
            foreach (ApplicationUser teammate in ViewBag.Roster)
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
            Team team = _db.Teams.Include(t => t.TeamManager).FirstOrDefault(t => t.Id == id);
            ViewBag.Team = team;
            List<Game> Schedule = _db.Games
                .Include(g => g.AvailablePlayers)
                    .ThenInclude(a => a.AppUser)
                .Where(g => g.Team == team)
                .OrderBy(g => g.Date)
                .ToList();
            return View(Schedule);
        }

        [HttpPatch]
        public IActionResult UpdateAvailability(int id, int teammate, bool playerAvailability)
        {
            //
            Team team = _db.Teams.FirstOrDefault(t => t.Id == id);
            Availability player = _db.Availabilities.Include(p => p.Game)
                .FirstOrDefault(pt => pt.Id == teammate);

            //
            player.Available = playerAvailability;
            _db.Entry(player).State = EntityState.Modified;
            _db.SaveChanges();

            //
            Game game = _db.Games
                .Include(g => g.AvailablePlayers)
                    .ThenInclude(a => a.AppUser)
                .Where(g => g.Team == team)
                .OrderBy(g => g.Date)
                .FirstOrDefault(g => g.Id == player.Game.Id);

            int availablePlayers = 0;
            foreach (Availability availablePlayer in game.AvailablePlayers)
            {
                if (availablePlayer.Available)
                {
                    availablePlayers++;
                }
            }

            return Content(availablePlayers.ToString());
        }

        public IActionResult AddGame(int id)
        {
            Team team = _db.Teams.Include(t => t.TeamManager).FirstOrDefault(t => t.Id == id);
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
            foreach (ApplicationUser teammate in roster)
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

        public async Task<IActionResult> Chat(int id)
        {
            ViewBag.User = await _userManager.GetUserAsync(User);
            ViewBag.Team = _db.Teams
                .Include(t => t.Messages)
                .Include(t => t.Roster)
                    .ThenInclude(r => r.AppUser)
                        .ThenInclude(u => u.Messages)
                .FirstOrDefault(t => t.Id == id);
            return View();
        }

        public IActionResult GetMessages(int id)
        {
            Team team = _db.Teams
                .Include(t => t.Messages)
                .Include(t => t.Roster)
                    .ThenInclude(r => r.AppUser)
                        .ThenInclude(u => u.Messages)
                .FirstOrDefault(t => t.Id == id);
            return View(team);
        }

        [HttpPost]
        public async Task<IActionResult> PostMessage(int id, string message)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            Team team = _db.Teams.FirstOrDefault(t => t.Id == id);
            ChatMessage newMessage = new ChatMessage()
            {
                Message = message,
                AppUser = user,
                Team = team,
                PostDateTime = DateTime.Today
            };
            _db.Messagese.Add(newMessage);
            _db.SaveChanges();
            team = _db.Teams
                .Include(t => t.Messages)
                .Include(t => t.Roster)
                    .ThenInclude(r => r.AppUser)
                        .ThenInclude(u => u.Messages)
                .FirstOrDefault(t => t.Id == id);
            return View("GetMessages", team);
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
