using PlanFood.Mvc.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanFood.Mvc.Services.Interfaces
{
    public interface IRecipeService
    {
        bool Create(Recipe recipe);
        Recipe Get(int id);
        IList<Recipe> GetAll();
        bool Update(Recipe recipe);
        bool Delete(int id);
        public int countAddedByUser(string userName);
        IList<Recipe> getUserRecipes(string userName);
    }
}
