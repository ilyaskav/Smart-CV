using System.ComponentModel.DataAnnotations;

namespace SmartCV.Service.Models
{
    public class LanguageModel
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Level { get; set; }
    }
}
