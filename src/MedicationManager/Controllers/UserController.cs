using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MedicationManager.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicationManager.Controllers
{
    public class UserController : Controller
    {
        public static string username;

        // GET: /<controller>/
        [Route("/Login")]
        public IActionResult Index()
        {
            ViewBag.username = username;
            return View();
        }

        public IActionResult Add()
        {
            AddUserViewModel addUserViewModel = new AddUserViewModel();
            return View(addUserViewModel);
        }

        [HttpPost]
        [Route("/Login/Add")]
        public IActionResult Add(AddUserViewModel addUserViewModel)
        {
            if (ModelState.IsValid)
            {
                username = addUserViewModel.Username;

                //TODO: Create User

                return Redirect("/MedHome");
            }

            return View(addUserViewModel);
        }
    }
}
