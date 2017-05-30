using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicationManager.ViewModels
{
    public class UserIndexViewModel
    {
        [Required]
        [Display(Name = "Username: ")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password: ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ErrorMessage { get; set; } = "empty";
    }
}
