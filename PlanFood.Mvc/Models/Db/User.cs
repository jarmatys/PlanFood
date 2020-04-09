using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PlanFood.Mvc.Models.Db
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
        public ICollection<Plan> Plans { get; set; }
    }
}
