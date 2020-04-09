using PlanFood.Mvc.Models.Db;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlanFood.Mvc.Models
{
    public class Plan
    {
        public int Id { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required, MaxLength]
        public string Description { get; set; }

        [Required, StringLength(45)]
        public string Name { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        public ICollection<RecipePlans> RecipePlans { get; set; }

    }
}
