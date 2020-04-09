using Microsoft.EntityFrameworkCore;
using PlanFood.Mvc.Context;
using PlanFood.Mvc.Models;
using PlanFood.Mvc.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanFood.Mvc.Services
{
    public class PlanService : IPlanService
    {
        private readonly PlanFoodContext _context;

        public PlanService(PlanFoodContext context)
        {
            _context = context;
        }

        public bool Create(Plan planModel)
        {
            _context.Plans.Add(planModel);
            return _context.SaveChanges() > 0;
        }

        public Plan Get(int id)
        {
            var plan = _context.Plans.Include(p => p.RecipePlans).ThenInclude(rp => rp.DayName) // dołączam plany z dniami tygodnia
            .ThenInclude(r => r.RecipePlans).ThenInclude(r => r.Recipe) // dołączam plany z przepisami
            .Where(u => u.Id == id) // filtruje po id
            .FirstOrDefault();

            return plan;
        }

        public IList<Plan> GetAll()
        {
            return _context.Plans.ToList();
        }

        public bool Update(Plan planModel)
        {
            _context.Plans.Update(planModel);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var plan = _context.Plans.SingleOrDefault(b => b.Id == id);
            if (plan == null)
                return false;

            _context.Plans.Remove(plan);
            return _context.SaveChanges() > 0;
        }

        public Plan LastAddedPlan(string userName)
        {
            var plan = _context.Plans.Include(p => p.RecipePlans).ThenInclude(rp => rp.DayName) // dołączam plany z dniami tygodnia
                .ThenInclude(r => r.RecipePlans).ThenInclude(r => r.Recipe) // dołączam plany z przepisami
                .Where(u => u.User.UserName == userName) // filtruje po nazwie użytkownika
                .OrderByDescending(d => d.Created) // Układam malejąco
                .FirstOrDefault(); // Pobieram z góry czyli najnowszy plan

            return plan;
        }

        public int CountAddedByUser(string userName)
        {
            return _context.Plans.Where(a => a.User.UserName == userName).Count();
        }

        /*
         * W tej metodzie szukamy plany użytkownika po jego nazwie, co jest złe, ale robie tak, żeby trzymać się konwencji jak w metodach
         * LastAddedPlan(string UserName) oraz CountAddedByUser(string userName).
         * Powinno się szukać po userId, bo to jest unikalny klucz, a właściwość userName w bazie nie musi być unikalna (przez co użytkownik
         * może zobaczyć plany innego użytkownika, co więcej userName może być nullem.
         * szukanie po userName to potencjalny bug
        */
        public IList<Plan> GetUserPlans(string userName)
        {
            var plans = _context.Plans
                .Where(p => p.User.UserName == userName)
                .ToList();
            return plans;
        }
    }
}
