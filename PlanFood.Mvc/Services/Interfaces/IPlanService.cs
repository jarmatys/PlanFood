using PlanFood.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanFood.Mvc.Services.Interfaces
{
    public interface IPlanService
    {
        bool Create(Plan planModel);
        Plan Get(int id);
        IList<Plan> GetAll();
        bool Update(Plan planModel);
        bool Delete(int id);
        public Plan LastAddedPlan(string userName);
        public int CountAddedByUser(string userName);
        IList<Plan> GetUserPlans(string userName);

    }
}
