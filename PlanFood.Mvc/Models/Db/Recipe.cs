using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlanFood.Mvc.Models.Db
{
    public class Recipe
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public string Description { get; set; }

        [Required]
        public string Ingredients { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; }

        [Required]
        public string Preparation { get; set; }

        [Required]
        public int PreparationTime { get; set; }

        public DateTime Updated { get; set; }

        public int? UserId { get; set; }

        public User User { get; set; }

        public ICollection<RecipePlans> RecipePlans { get; set; }

    }
}
