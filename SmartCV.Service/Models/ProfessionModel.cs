using System.ComponentModel.DataAnnotations;

namespace SmartCV.Service.Models
{
    public class ProfessionModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Rule { get; set; }
    }
}
