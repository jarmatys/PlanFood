using Microsoft.EntityFrameworkCore;
using PlanFood.Mvc.Context;
using PlanFood.Mvc.Models.Db;
using PlanFood.Mvc.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanFood.Mvc.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly PlanFoodContext _context;

        public RecipeService(PlanFoodContext context)
        {
            _context = context;
        }

        public bool Create(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            return _context.SaveChanges() > 0;
        }

        public Recipe Get(int id)
        {
            return _context.Recipes.SingleOrDefault(x => x.Id == id);
        }

        public IList<Recipe> GetAll()
        {
            return _context.Recipes.ToList();
        }

        public bool Update(Recipe recipe)
        {
            _context.Recipes.Update(recipe);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var recipe = _context.Recipes.SingleOrDefault(b => b.Id == id);
            if (recipe == null)
                return false;

            _context.Recipes.Remove(recipe);
            return _context.SaveChanges() > 0;
        }

        public int countAddedByUser(string userName)
        {
            return _context.Recipes.Where(a => a.User.UserName == userName).Count();
        }

        public IList<Recipe> getUserRecipes(string userName)
        {
            return _context.Recipes.Include(recipePlan => recipePlan.RecipePlans).
                Where(recipe => recipe.User.UserName == userName).
                OrderByDescending(recipe => recipe.Created).ToList();
        }

    }
}
