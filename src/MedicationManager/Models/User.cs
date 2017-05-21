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

        // public string PasswordHash { get; set; }

        public List<Medication> AllMeds { get; set; }

        public PermissionLevel PermissionLevel { get; set; }

        public string UserId { get; internal set; }
        
        /*
        public User(string username, string email, string password)
        {
            Username = username;
            Email = email;
            Password = password;
            UserId = Guid.NewGuid().ToString().Replace("-", string.Empty);
        } */

        public User()
        {
            UserId = Guid.NewGuid().ToString().Replace("-", string.Empty);
        }
    }
}
