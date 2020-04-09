using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlanFood.Mvc.Models.ViewModels
{
    public class RecipeViewModel
    {
        [Required(ErrorMessage = "Pole wymagane")]
        [StringLength(255)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        public int PreparationTime { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        public string Preparation { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        public string Ingredients { get; set; }
    }
}
