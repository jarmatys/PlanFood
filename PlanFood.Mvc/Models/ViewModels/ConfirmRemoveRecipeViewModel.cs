using PlanFood.Mvc.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanFood.Mvc.Models.ViewModels
{
    public class ConfirmRemoveRecipeViewModel
    {
        public Recipe Recipe { get; set; }
        public int RecipeId { get; set; }
    }
}
