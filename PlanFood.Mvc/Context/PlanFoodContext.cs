using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlanFood.Mvc.Models;
using PlanFood.Mvc.Models.Db;

namespace PlanFood.Mvc.Context
{
	public class PlanFoodContext : IdentityDbContext<User>
	{
		public PlanFoodContext(DbContextOptions<PlanFoodContext> options) : base(options) { }

		public DbSet<Book> Books { get; set; }
		public DbSet<Plan> Plans { get; set; }
		public DbSet<DayName> DayNames { get; set; }
		public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipePlans> RecipePlans { get; set; }

		// zaślepka na klasę bazową
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
		}
        
	}
}
