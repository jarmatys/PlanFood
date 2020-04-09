using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlanFood.Mvc.Models;
using PlanFood.Mvc.Models.Db;
using PlanFood.Mvc.Models.ViewModels;
using PlanFood.Mvc.Services.Interfaces;

namespace PlanFood.Mvc.Controllers
{
    [Authorize]
    public class PlanController : Controller
    {
        private readonly IPlanService _planService;
        private readonly UserManager<User> _userManager;
        private readonly IRecipePlansService _recipePlansService;
        private readonly IRecipeService _recipeService;
        private readonly IDayNameService _dayNameService;

        public PlanController(IPlanService planService, UserManager<User> userManager, IRecipePlansService recipePlansService, IRecipeService recipeService, IDayNameService dayNameService)
        {
            _planService = planService;
            _userManager = userManager;
            _recipePlansService = recipePlansService;
            _recipeService = recipeService;
            _dayNameService = dayNameService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewBag.userName = (await _userManager.GetUserAsync(User)).Name;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPlanViewModel planModel)
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.userName = user.Name;
            if (ModelState.IsValid)
            {
                try
                {
                    var plan = new Plan
                    {
                        Created = DateTime.Now,
                        User = user,
                        Name = planModel.PlanName,
                        Description = planModel.Description,
                    };
                    _planService.Create(plan);

                    return RedirectToAction("List");

                }
                catch
                {
                    return View(planModel);
                }
            }
            else
            {
                return View(planModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.userName = user.Name;

            var plan = _planService.Get(id);
            return View(plan);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.userName = user.Name;

            var planList = _planService.GetUserPlans(user.UserName);
            return View(planList);
        }

        [HttpGet]
        public async Task<IActionResult> AddRecipe()
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.userName = user.Name;

            ViewBag.Plans = _planService.GetUserPlans(user.UserName);
            ViewBag.Recipes = _recipeService.getUserRecipes(user.UserName);
            ViewBag.DayNames = _dayNameService.GetAll();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRecipe(AddPlanToRecipeViewModel result)
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.userName = user.Name;

            ViewBag.Plans = _planService.GetUserPlans(user.UserName);
            ViewBag.Recipes = _recipeService.getUserRecipes(user.UserName);
            ViewBag.DayNames = _dayNameService.GetAll();

            if (ModelState.IsValid)
            {
                try
                {
                    var addRecipe = new RecipePlans
                    {
                        DisplayOrder = result.FoodOrder,
                        MealName = result.FoodName,

                        Plan = _planService.Get(result.PlanId),
                        Recipe = _recipeService.Get(result.RecipeId),
                        DayName = _dayNameService.Get(result.DayNameId)
                    };
                    _recipePlansService.Create(addRecipe);
                    return RedirectToAction("Index", "Dashboard");
                }
                catch
                {
                    return View(result);
                }
            }
            else
            {
                return View(result);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.userName = user.Name;

            var plan = _planService.Get(id);
            return View(plan);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Plan planModel)
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.userName = user.Name;

            if (ModelState.IsValid)
            {
                try
                {
                    _planService.Update(planModel);
                    return RedirectToAction("List");
                }
                catch
                {
                    return View("Edit", planModel);
                }
            }
            else
            {
                return View("Edit", planModel);
            }
        }

        [HttpGet]
        public IActionResult ConfirmRemoveRecipeFromPlan(int planId, int recipeId, int recipePlanId, [FromServices]ConfirmRemoveRecipeFromPlanViewModel model)
        {
            model.Recipe = _recipeService.Get(recipeId);
            model.Plan = _planService.Get(planId);
            model.RecipePlanId = recipePlanId;

            return View(model);
        }

        [HttpPost]
        public IActionResult RemoveRecipeFromPlan([FromForm]ConfirmRemoveRecipeFromPlanViewModel model)
        {
            try
            {
                _recipePlansService.Delete(model.RecipePlanId);
            }
            catch
            {
                return RedirectToAction("ConfirmRemoveRecipeFromPlan", new { planId = model.Plan.Id, recipeId = model.Recipe.Id, recipePlanId = model.RecipePlanId });
            }
            return RedirectToAction("Details", new { id = model.Plan.Id });
        }

        [HttpPost]
        public IActionResult RemovePlan([FromForm]ConfirmRemovePlanViewModel model)
        {
            try
            {
                _planService.Delete(model.PlanId);
            }
            catch
            {
                return RedirectToAction("ConfirmRemovePlan", new { id = model.PlanId });
;           }
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult ConfirmRemovePlan(int id, [FromServices]ConfirmRemovePlanViewModel model)
        {
            model.Plan = _planService.Get(id);
            if(model.Plan == null) { return RedirectToAction("List"); }

            return View(model);
        }
    }
}