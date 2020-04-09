using PlanFood.Mvc.Context;
using PlanFood.Mvc.Models.Db;
using PlanFood.Mvc.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanFood.Mvc.Services
{
    public class DayNameService : IDayNameService
    {
        private readonly PlanFoodContext _context;

        public DayNameService(PlanFoodContext context)
        {
            _context = context;
        }

        public IList<DayName> GetAll()
        {
            return _context.DayNames.ToList();
        }

        public DayName Get(int id)
        {
            return _context.DayNames.Find(id);
        }
    }
}
