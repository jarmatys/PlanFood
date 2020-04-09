using PlanFood.Mvc.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanFood.Mvc.Models.ViewModels
{
    public class ConfirmRemoveRecipeFromPlanViewModel
    {
        public Plan Plan { get; set; }
        public Recipe Recipe { get; set; }

        public int RecipePlanId { get; set; }
    }
}
