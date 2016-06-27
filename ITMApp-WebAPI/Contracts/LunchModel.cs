using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class LunchModel
    {
        public int Id { get; set; }
        [Required]
        public int RestaurantId { get; set; }
        public List<EmployeeModel> EmployeesList { get; set; }
        [Required]
        public DateTime LunchTime { get; set; }
        [Required]
        public RestaurantModel Restaurant { get; set; }
        public bool Removed { get; set; }

    }
}
