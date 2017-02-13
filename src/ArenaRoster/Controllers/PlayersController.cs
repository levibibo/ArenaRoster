//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using ArenaRoster.Models;
//using System.Diagnostics;
//using Microsoft.AspNetCore.Http;
//using System.IO;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;

//// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

//namespace ArenaRoster.Controllers
//{
//    [Authorize]
//    public class PlayersController : Controller
//    {
//        private readonly ArenaRosterDbContext _db;
//        private readonly UserManager<ApplicationUser> _userManager;

//        public PlayersController(UserManager<ApplicationUser> userManager, ArenaRosterDbContext db)
//        {
//            _userManager = userManager;
//            _db = db;
//        }

//        public IActionResult Index()
//        {
//            return View(_db.Players.ToList());
//        }

//        public IActionResult Create()
//        {
//            return View();
//        }
//        [HttpPost]
//        public IActionResult Create(string name, string email, string phone, IFormFile picture)
//        {
//            byte[] pictureArray = new byte[0];
//            if (picture.Length > 0)
//            {
//                using (var fileStream = picture.OpenReadStream())
//                using (var ms = new MemoryStream())
//                {
//                    fileStream.CopyTo(ms);
//                    pictureArray = ms.ToArray();
//                }
//            }
//            Player newPlayer = new Models.Player(name, email, phone, pictureArray);
//            _db.Players.Add(newPlayer);
//            _db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        public IActionResult Details(int id)
//        {
//            return View(_db.Players.Include(users => users.PlayerTeams)
//                .FirstOrDefault(users => users.PlayerId == id));
//        }
//    }
//}
