using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicationManager.ViewModels
{
    public class AddUserViewModel
    {
        [Display(Name = "Username: ")]
        [Required]
        public string Username { get; set; }

        [Display(Name = "Full Name")]
        [Required]
        public string FullName { get; set; }

        [Display(Name = "Email Address: ")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Password: ")]
        [DataType(DataType.Password)]
        [StringLength(25, ErrorMessage = "Must be between 6 and 25 characters", MinimumLength = 6)]
        [Required]
        public string Password { get; set; }

        [Display(Name = "Verify Password: ")]
        [DataType(DataType.Password)]
        [StringLength(25, ErrorMessage = "Must be between 6 and 25 characters", MinimumLength = 6)]
        [Compare("Password", ErrorMessage = "The passwords you entered do not match!")]
        [Required]
        public string VerifyPassword { get; set; }

        public string UserId { get; set; }
        
        public AddUserViewModel()
        {

        }
    }
}
