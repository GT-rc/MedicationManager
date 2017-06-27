using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MedicationManager.Data;
using MedicationManager.ViewModels.Meds;
using MedicationManager.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicationManager.Controllers
{
    // [Authorize] --- using this I get a forbidden error when I login, without it I just keep going 
    // back to the login page...
    public class MedicationController : Controller
    {
        // Set up db context
        private readonly ApplicationDbContext context;

        public MedicationController(ApplicationDbContext dbContext)
        {
            this.context = dbContext;
        }

        // public static User UserLoggedIn => HttpContext.Authentication.GetAuthenticateInfoAsync.User;
        // TODO: Figure out how to set this variable to the user who logged in in the UserController

        // GET: /<controller>/
        [Route("/MedHome")]
        public IActionResult Index()
        {
            User userLoggedIn;

            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                userLoggedIn = context.Users.Single(c => c.Username == userName);
            }
            else
            {
                return Redirect("/Login");
            }

            IList<Medication> userMeds = userLoggedIn.AllMeds;

            return View(userMeds);
        }

        
        // To Add a new medication to your list.
        public IActionResult Add()
        {
            IEnumerable<ToD> times = (ToD[])Enum.GetValues(typeof(ToD));
            
            AddMedViewModel addMedViewModel = new AddMedViewModel(times);

            return View(addMedViewModel);
        }
        
        [HttpPost]
        public IActionResult Add(AddMedViewModel addMedViewModel)
        {
            User userLoggedIn;

            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                userLoggedIn = context.Users.Single(c => c.Username == userName);
            }
            else
            {
                return Redirect("/Login");
            }
            
            if (ModelState.IsValid)
            {
                Medication newMed = new Medication
                {
                    Name = addMedViewModel.Name,
                    Dosage = addMedViewModel.Dosage,
                    TimesXDay = addMedViewModel.TimesXDay,
                    Notes = addMedViewModel.Notes,
                    TimeOfDay = addMedViewModel.ToDID,
                    // TODO: will need to look up value of tod id -- UPDATE: Will pass as hidden value set in VM
                    Description = addMedViewModel.Description,
                    RefillRate = addMedViewModel.RefillRate,
                    UserId = addMedViewModel.UserId
                };

                // TODO: add to db
                context.Medication.Add(newMed);
                // TODO: add to UserLoggedIn medication list
                userLoggedIn.AllMeds.Add(newMed);

                // save changes
                context.SaveChanges();

                return Redirect("/Medication/Index");
            }

            return View(addMedViewModel);
        }
        /*
        public IActionResult Remove()
        {
            ViewBag.meds = 0;
            // TODO: query db for list of all users meds, pass into ViewBag
            return View();
        }

        [HttpPost]
        public IActionResult Remove(int[] medIds)
        {
            foreach (int id in medIds)
            {
                // TODO: find med in list
                // TODO: remove med from list
            }

            // TODO: save changes

            return Redirect("Medication/Index");
        }

        public IActionResult Edit(int id)
        {
            Medication med = context.Medication.Single(c => c.MedID == id);
            
            EditMedViewModel editMedViewModel = new EditMedViewModel(med);
            return View(editMedViewModel);
        }

        [HttpPost]
        public IActionResult Edit(int medId, EditMedViewModel editMedViewModel)
        {
            Medication editedMed = context.Medication.Single(c => c.MedID == medId);

            editedMed.Name = editMedViewModel.Med.Name;
            editedMed.Dosage = editMedViewModel.Med.Dosage;
            editedMed.Notes = editMedViewModel.Med.Notes;
            editedMed.TimesXDay = editMedViewModel.Med.TimesXDay;
            editedMed.TimeOfDay = editMedViewModel.Med.TimeOfDay;
            editedMed.Description = editMedViewModel.Med.Description;
            editedMed.RefillRate = editMedViewModel.Med.RefillRate;

            // TODO: update change and save to db

            return Redirect("Medication/Index");
        }

        // TODO: Create methods and logic for printing out list of meds.  */
    }
}
