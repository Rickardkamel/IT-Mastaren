
using System;
using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class EmployeeAbsenceModel
    {
        public int Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Required]
        public EmployeeModel Employee { get; set; }
        [Required]
        public AbsenceModel Absence { get; set; }
        [Required]
        public bool Removed { get; set; }
        
    }
}
