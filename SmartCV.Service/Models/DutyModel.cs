using System.ComponentModel.DataAnnotations;

namespace SmartCV.Service.Models
{
    public class DutyModel
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int? WorkPlaceId { get; set; }
    }
}
