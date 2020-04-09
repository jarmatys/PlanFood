using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlanFood.Mvc.Context;
using PlanFood.Mvc.Models.Db;
using PlanFood.Mvc.Models.ViewModels;
using PlanFood.Mvc.Services;
using PlanFood.Mvc.Services.Interfaces;

namespace PlanFood.Mvc
{
	public class Startup
	{
		// Pobieranie pliku konfiguracyjnego 
		public IConfiguration Configuration { get; }
		public Startup()
		{
			var config = new ConfigurationBuilder();
			config.AddJsonFile("appsettings.json");
			Configuration = config.Build();
		}

		public void ConfigureServices(IServiceCollection services)
		{
			// Łączenie z bazą danych
			services.AddDbContext<PlanFoodContext>(builder =>
			{
				builder.UseSqlServer(Configuration["Database:ConnectionString"]);
			});

			// Wstrzykiwanie zależności o identifykacji użytkowników
			services.AddIdentity<User, IdentityRole>(options =>
			{
				// Opcje dotyczące hasła
				options.Password.RequireDigit = false;
				options.Password.RequiredLength = 2;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireLowercase = false;

			}).AddEntityFrameworkStores<PlanFoodContext>();

			// tutaj wsztrzykujemy inteface z servisem który obsługuje dany model
			services.AddScoped<IBookService, BookService>();
			services.AddScoped<IDayNameService, DayNameService>();
			services.AddScoped<IRecipePlansService, RecipePlansService>();
			services.AddScoped<IPlanService, PlanService>();
			services.AddScoped<IRecipeService, RecipeService>();

			services.AddScoped<ConfirmRemoveRecipeFromPlanViewModel>();
			services.AddScoped<ConfirmRemovePlanViewModel>();
			services.AddScoped<ConfirmRemoveRecipeViewModel>();
			services.AddScoped<PlanDetailsViewModel>();

			services.AddRazorPages();
			services.AddControllersWithViews();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseStaticFiles();
			app.UseAuthentication();
			app.UseRouting();
			app.UseAuthorization();						

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapDefaultControllerRoute();
			});
		}
	}
}
