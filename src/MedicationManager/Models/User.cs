using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicationManager.Models
{
    public class User
    {
        public string FullName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PasswordHash { get; set; }

        public List<Medication> AllMeds { get; set; }

        public PermissionLevel PermissionLevel { get; set; } = PermissionLevel.User;

        public string UserId { get; internal set; }
        
        public User()
        {
            AllMeds = new List<Medication>();
            UserId = Guid.NewGuid().ToString().Replace("-", string.Empty);
        }
    }
}
