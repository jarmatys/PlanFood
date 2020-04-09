using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanFood.Mvc.Models.Db
{
    public class DayName
    {
        public int Id { get; set; }
        public int DisplayOrder { get; set; }
        public string Name { get; set; }
        public ICollection<RecipePlans> RecipePlans { get; set; }
    }
}
