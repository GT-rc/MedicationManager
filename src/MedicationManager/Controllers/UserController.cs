using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MedicationManager.ViewModels;
using MedicationManager.Models;
using MedicationManager.Data;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicationManager.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext context;

        public UserController(ApplicationDbContext dbContext)
        {
            this.context = dbContext;
        }

        public static string username;

        // GET: /<controller>/
        [Route("/Login")]
        public IActionResult Login()
        {
            ViewBag.username = username;
            return View();
        }

        // POST -- Login a user
        [Route("/Login")]
        public IActionResult Login(UserIndexViewModel userIndexViewModel)
        {
            User loginTest = context.Users.Single(c => c.Username == userIndexViewModel.Username);

            if (ModelState.IsValid)
            {
                // TODO: Add pw hash/salt to check logins against
            }

            return View(userIndexViewModel);
        }

        // GET -- Add a new user
        [Route("/Login/Add")]
        public IActionResult Add()
        {
            AddUserViewModel addUserViewModel = new AddUserViewModel();
            return View(addUserViewModel);
        }

        // POST -- Add a new user
        [HttpPost]
        [Route("/Login/Add")]
        public IActionResult Add(AddUserViewModel addUserViewModel)
        {
            if (ModelState.IsValid)
            {
                User newUser = new Models.User
                {
                    FullName = addUserViewModel.FullName,
                    Username = addUserViewModel.Username,
                    Email = addUserViewModel.Email,
                    Password = addUserViewModel.Password
                };

                // TODO: Check to verify the user is not already in the db, if not, add them

                context.Users.Add(newUser);
                context.SaveChanges();

                return Redirect("/MedHome");
            }

            return View(addUserViewModel);
        }
    }
}
