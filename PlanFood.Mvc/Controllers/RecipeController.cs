using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlanFood.Mvc.Models.Db;
using Microsoft.AspNetCore.Authorization;
using PlanFood.Mvc.Services.Interfaces;
using PlanFood.Mvc.Models.ViewModels;

namespace PlanFood.Mvc.Controllers
{
    [Authorize]
    public class RecipeController : Controller
    {
        private IRecipeService _recipeService { get; }
        private readonly UserManager<User> _userManager;

        public RecipeController(UserManager<User> userManager, IRecipeService recipeService)
        {
            _userManager = userManager;
            _recipeService = recipeService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            ViewBag.userName = (await _userManager.GetUserAsync(User)).Name;

            var user = await _userManager.GetUserAsync(User);
            var recipeList = _recipeService.getUserRecipes(user.UserName);
            return View(recipeList);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ViewBag.userName = (await _userManager.GetUserAsync(User)).Name;

            var recipe = _recipeService.Get(id);
            return View(recipe);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewBag.userName = (await _userManager.GetUserAsync(User)).Name;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(RecipeViewModel recipeModel)
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.userName = user.Name;

            if (ModelState.IsValid)
            {
                try
                {
                    var recipe = new Recipe
                    {
                        Created = DateTime.Now,
                        Description = recipeModel.Description,
                        Ingredients = recipeModel.Ingredients,
                        Name = recipeModel.Name,
                        Preparation = recipeModel.Preparation,
                        PreparationTime = recipeModel.PreparationTime,
                        User = user,
                    };
                    _recipeService.Create(recipe);
                    return RedirectToAction("list", "recipe");
                }
                catch
                {
                    return View(recipeModel);
                }
            }
            else
            {
                return View(recipeModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.userName = user.Name;

            var plan = _recipeService.Get(id);
            return View(plan);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Recipe recipeModel)
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.userName = user.Name;

            if (ModelState.IsValid)
            {
                try
                {
                    _recipeService.Update(recipeModel);
                    return RedirectToAction("List");
                }
                catch
                {
                    return View("Edit", recipeModel);
                }
            }
            else
            {
                return View("Edit", recipeModel);
            }
        }

        [HttpPost]
        public IActionResult RemoveRecipe([FromForm]ConfirmRemoveRecipeViewModel model)
        {
            try
            {
                _recipeService.Delete(model.RecipeId);
            }
            catch
            {
                return RedirectToAction("ConfirmRemoveRecipe", new { id = model.RecipeId });
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult ConfirmRemoveRecipe(int id, [FromServices]ConfirmRemoveRecipeViewModel model)
        {
            model.Recipe = _recipeService.Get(id);
            if(model.Recipe == null) { return RedirectToAction("List"); }

            return View(model);
        }
    }
}
