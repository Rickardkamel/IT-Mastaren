using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class LunchModel
    {
        public int Id { get; set; }
        public List<EmployeeModel> EmployeesList { get; set; }
        public DateTime LunchTime { get; set; }
        public RestaurantModel Restaurant { get; set; }
        public bool Removed { get; set; }

    }
}
