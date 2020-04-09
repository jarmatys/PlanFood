using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlanFood.Mvc.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Pole wymagane")]
        [EmailAddress(ErrorMessage = "Wprowadzono błędny adres email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        public string Password { get; set; }
    }
}
