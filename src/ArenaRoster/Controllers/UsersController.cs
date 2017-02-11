using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArenaRoster.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.IO;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ArenaRoster.Controllers
{
    public class UsersController : Controller
    {
        private ArenaRosterDbContext db = new ArenaRosterDbContext();
        public IActionResult Index()
        {
            return View(db.Users.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(string name, string email, string phone, IFormFile picture)
        {
            byte[] pictureArray = new byte[0];
            if (picture.Length > 0)
            {
                using (var fileStream = picture.OpenReadStream())
                using (var ms = new MemoryStream())
                {
                    fileStream.CopyTo(ms);
                    pictureArray = ms.ToArray();
                }
            }
            User newUser = new Models.User(name, email, phone, pictureArray);
            db.Users.Add(newUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            return View(db.Users.Include(users => users.UserTeams)
                .FirstOrDefault(users => users.UserId == id));
        }
    }
}
