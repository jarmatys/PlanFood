using PlanFood.Mvc.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanFood.Mvc.Services.Interfaces
{
    public interface IDayNameService
    {  
        IList<DayName> GetAll();
        DayName Get(int id);
    }
}
