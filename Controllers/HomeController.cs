using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LogRegMay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace LogRegMay.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MyContext _context;

        public HomeController(ILogger<HomeController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("register")]
        public IActionResult Register(User newUser)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.email == newUser.email))
                {
                    ModelState.AddModelError("email", "Email is already in use!");
                    return View("Index");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.password = Hasher.HashPassword(newUser, newUser.password);
                _context.Add(newUser);
                _context.SaveChanges();
                return RedirectToAction("Success");
            }
            else
            {
                return View("Index");
            }
        }
        [HttpGet("success")]
        public IActionResult Success()
        {
            return View();
        }

        [HttpPost("login")]
        public IActionResult Login(LogUser logUser)
        {
            if (ModelState.IsValid)
            {
                User userinDb = _context.Users.FirstOrDefault(u => u.email == logUser.lemail);
                if(userinDb == null)
                {
                    ModelState.AddModelError("lemail","Invalid login attempt");
                    return View("Index");
                }
                PasswordHasher<LogUser> Hasher = new PasswordHasher<LogUser>();
                PasswordVerificationResult result = Hasher.VerifyHashedPassword(logUser, userinDb.password, logUser.lpassword);
                if(result ==0)
                {
                    ModelState.AddModelError("lemail","Invalid login attempt");
                    return View("Index");
                }
                return RedirectToAction("Success");
            }
            else
            {
                return View("Index");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
