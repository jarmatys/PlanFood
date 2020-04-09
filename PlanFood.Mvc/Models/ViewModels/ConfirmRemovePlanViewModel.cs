using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanFood.Mvc.Models.ViewModels
{
    public class ConfirmRemovePlanViewModel
    {
        public Plan Plan { get; set; }
        public int PlanId { get; set; }
    }
}
