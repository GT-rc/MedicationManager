using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicationManager.Models
{
    public class Set
    {
        public int SetID { get; set; }
        public List<Medication> MedList { get; set; }
        public ToD TimeOfDay { get; set; }

    }
}
