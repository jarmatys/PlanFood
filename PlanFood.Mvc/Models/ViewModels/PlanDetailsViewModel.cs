using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanFood.Mvc.Models.ViewModels
{
    public class PlanDetailsViewModel
    {
        public string PlanName { get; set; }

        public List<DayNameViewModel> DayNames { get; set; }
    }

    public class DayNameViewModel
    {
        public string DayName { get; set; }
        public List<RecipeView> RecipeList { get; set; }
    } 

    public class RecipeView
    {
        public int Id { get; set; }
        public string MealName { get; set; }
        public string RecipeName { get; set; }
    }
}
