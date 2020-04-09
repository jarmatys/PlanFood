using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlanFood.Mvc.Models.ViewModels
{
    public class AddPlanViewModel
    {
        [Required(ErrorMessage = "Pole wymagane")]
        public string PlanName { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        public string Description { get; set; }
    }
}
