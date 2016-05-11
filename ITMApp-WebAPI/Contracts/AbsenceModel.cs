using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class AbsenceModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
