using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlanFood.Mvc.Models.Db;

namespace PlanFood.Mvc.Services.Interfaces
{
    public interface IRecipePlansService
    {
        bool Create(RecipePlans recipePlans);
        RecipePlans Get(int id);
        IList<RecipePlans> GetAll();
        bool Update(RecipePlans recipePlans);
        bool Delete(int id);

    }
}
