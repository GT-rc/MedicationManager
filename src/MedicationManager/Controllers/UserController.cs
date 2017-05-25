﻿using System;
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
        public IActionResult Index()
        {
            ViewBag.username = username;
            return View();
        }

        // GET -- Add a new user
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

                context.Users.Add(newUser);
                context.SaveChanges();

                return Redirect("/MedHome");
            }

            return View(addUserViewModel);
        }
    }
}
