using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicationManager.Models
{
    public class MedSets
    {
        public int ID { get; set; }

        public int SetId { get; set; }
        public Set MedSet { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

    }
}
