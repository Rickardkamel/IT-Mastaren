using System;
using System.Collections.Generic;

namespace Contracts
{
    public class LunchModel
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public List<EmployeeModel> EmployeesList { get; set; }
        public DateTime LunchTime { get; set; }
        public RestaurantModel Restaurant { get; set; }
        public bool Removed { get; set; }

    }
}
