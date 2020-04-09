using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanFood.Mvc.Models.ViewModels
{
    public class AddPlanToRecipeViewModel
    {
        public int PlanId { get; set; }

        public string FoodName { get; set; }

        public int FoodOrder { get; set; }

        public int RecipeId { get; set; }

        public int DayNameId { get; set; }
    }
}
