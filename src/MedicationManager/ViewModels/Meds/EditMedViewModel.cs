using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MedicationManager.Models;

namespace MedicationManager.ViewModels.Meds
{
    public class EditMedViewModel
    {
        [Required]
        public int MedId { get; set; }

        public Medication Med { get; set; }
        
        public EditMedViewModel() : base() { }

        public EditMedViewModel(Medication med)
        {
            Med = med;
        }
    }
}
