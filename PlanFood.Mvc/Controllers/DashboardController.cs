using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlanFood.Mvc.Models.Db;
using PlanFood.Mvc.Models.ViewModels;
using PlanFood.Mvc.Services;
using PlanFood.Mvc.Services.Interfaces;

namespace PlanFood.Mvc.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private IPlanService _planService { get; }
        private UserManager<User> _userManager { get;  }
        private IRecipeService _recipeService { get;  }
        private IDayNameService _dayNameService { get;  }

        public DashboardController(IPlanService planService, UserManager<User> userManager, IRecipeService recipeService, IDayNameService dayNameService)
        {
            _userManager = userManager;
            _planService = planService;
            _recipeService = recipeService;
            _dayNameService = dayNameService;
        }

        public async Task<IActionResult> Index([FromServices]PlanDetailsViewModel toView)
        {
            //akutalnie zalogowany użytkownik
            var user = await _userManager.GetUserAsync(User);
            
            // pobieramy jego ostatni dodany plan
            var plan = _planService.LastAddedPlan(user.UserName);

            if(plan != null)
            {
                var days = _dayNameService.GetAll();

                toView.PlanName = plan.Name;
                var dayNamesList = new List<DayNameViewModel>();

                foreach (var day in days)
                {
                    var recipesInDay = plan.RecipePlans.Where(a => a.DayName.Id == day.Id);
                    if (recipesInDay.Count() >= 1)
                    {
                        var tempDay = new DayNameViewModel { DayName = day.Name };
                        var recipesList = new List<RecipeView>();
                        foreach (var recipe in recipesInDay)
                        {
                            var tempRecipes = new RecipeView();

                            tempRecipes.Id = recipe.Recipe.Id;
                            tempRecipes.MealName = recipe.MealName;
                            tempRecipes.RecipeName = recipe.Recipe.Name;

                            recipesList.Add(tempRecipes);
                        }
                        tempDay.RecipeList = recipesList;
                        dayNamesList.Add(tempDay);
                    }
                }
                toView.DayNames = dayNamesList;
            }
            else { toView = null; }

            //przekazujemy do widoku liczbę dodanych planów i przepisów
            ViewBag.AddedRecipie = _recipeService.countAddedByUser(user.UserName);
            ViewBag.AddedPlans = _planService.CountAddedByUser(user.UserName);
            ViewBag.userName = user.Name; // do _LoginPartial.cshtml

            return View(toView);
        }
    }
}
