using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlanFood.Mvc.Models.Db
{
    public class RecipePlans
    {
        [Key]
        public int Id { get; set; }     
        [Required]
        public int DisplayOrder { get; set; }
        [Required]
        public string MealName { get; set; }

        public int PlanId { get; set; }
        [Required]
        public Plan Plan { get; set; }

        public int RecipeId { get; set; }
        [Required]
        public Recipe Recipe { get; set; }

        public int DayNameId { get; set; }
        [Required]
        public DayName DayName { get; set; }

    }
}
