using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MedicationManager.ViewModels;
using MedicationManager.Models;
using MedicationManager.Data;
using System.Security.Claims;

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
            UserIndexViewModel userIndexViewModel = new UserIndexViewModel();
            ViewBag.username = username;
            return View(userIndexViewModel);
        }

        // POST -- Login a user
        [HttpPost]
        [Route("/Login")]
        public async Task<IActionResult> Login(UserIndexViewModel userIndexViewModel)
        {
            
            if (ModelState.IsValid)
            {
                // VerifyHash
                User isUser = context.Users.SingleOrDefault(c => c.Username == userIndexViewModel.Username);

                if (isUser != null)
                {
                    bool isValid = HashUtils.VerifyHash(isUser.Password, "SHA256", isUser.PasswordHash);

                    if (isValid == true)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, isUser.Username),
                            new Claim(ClaimTypes.Hash, isUser.PasswordHash)
                        };

                        var userIdentity = new ClaimsIdentity(claims);

                        var userPrincipal = new ClaimsPrincipal(userIdentity);

                        await HttpContext.Authentication.SignInAsync("MedicationCookieMiddlewareInstance", userPrincipal, 
                            new Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties {
                                ExpiresUtc = DateTime.UtcNow.AddMinutes(60),
                                IsPersistent = false,
                                AllowRefresh = false });
                        
                        

                        return Redirect("/MedHome");
                    }
                    else
                    {
                        userIndexViewModel.ErrorMessage = "Login Information is not valid. Please retry.";
                        return View(userIndexViewModel);
                    }
                }
                else
                {
                    userIndexViewModel.ErrorMessage = "User Information is not valid. Please retry.";
                    return View(userIndexViewModel);
                }
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

                // ComputeHash
                string newUserPwHash = HashUtils.ComputeHash(newUser.Password, null, null);
                newUser.PasswordHash = newUserPwHash;
                // TODO: Check to verify the user is not already in the db, if not, add them
                var inDatabaseAlready = context.Users.SingleOrDefault(c => c.FullName == newUser.FullName);
                if (inDatabaseAlready == null)
                {
                    context.Users.Add(newUser);
                    context.SaveChanges();

                    return Redirect("/MedHome");
                }
                else
                {
                    addUserViewModel.ErrorMessage = "Your name already exixts. Do you have an account?";
                    return View(addUserViewModel);
                }
                
            }

            return View(addUserViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("MedicationCookieMiddlewareInstance");

            return Redirect("/Login");
        }

        // Next Steps: Add Edit User
        
    }
}
