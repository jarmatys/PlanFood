using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlanFood.Mvc.Context;
using PlanFood.Mvc.Models.Db;
using PlanFood.Mvc.Services.Interfaces;

namespace PlanFood.Mvc.Services
{
    public class RecipePlansService : IRecipePlansService
    {
        private readonly PlanFoodContext _context;

        public RecipePlansService(PlanFoodContext context)
        {
            _context = context;
        }

        public bool Create(RecipePlans recipePlans)
        {
            _context.RecipePlans.Add(recipePlans);
            return _context.SaveChanges() > 0;
        }

        public RecipePlans Get(int id)
        {
            return _context.RecipePlans.SingleOrDefault(b => b.Id == id);
        }

        public IList<RecipePlans> GetAll()
        {
            return _context.RecipePlans.ToList();
        }

        public bool Update(RecipePlans recipePlans)
        {
            _context.RecipePlans.Update(recipePlans);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var reciple = _context.RecipePlans.SingleOrDefault(b => b.Id == id);
            if (reciple == null)
                return false;

            _context.RecipePlans.Remove(reciple);
            return _context.SaveChanges() > 0;
        }
    }
}
