using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlanFood.Mvc.Models.Db;
using PlanFood.Mvc.Services.Interfaces;

namespace PlanFood.Mvc.Controllers
{
	public class HomeController : Controller
	{
		private IRecipeService _recipeService { get; }

		public HomeController( IRecipeService recipeService)
		{
			_recipeService = recipeService;
		}

		public IActionResult Index()
		{
		    return View();
		}

		public IActionResult Contact()
		{
			return View();
		}

		public IActionResult About()
		{
			return View();
		}

		public IActionResult Recipes()
		{
			var recipeList = _recipeService.GetAll();
			return View(recipeList);
		}

		public IActionResult RecipeDetails(int id)
		{
			var recipe = _recipeService.Get(id);
			return View(recipe);
		}
	}
}