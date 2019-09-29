using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartCV.Service.Models
{
    public class WorkPlaceAddModel
    {
        public int? ResumeManagerId { get; set; }

        [UIHint("WorkPlaces")]
        public ICollection<WorkPlaceModel> WorkPlaces { get; set; }

        public WorkPlaceAddModel()
        {
            WorkPlaces = new List<WorkPlaceModel>();
        }
    }
}