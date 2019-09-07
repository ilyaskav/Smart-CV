using Entities.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Services.Models
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