using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class WorkPlaceModel
    {
        public int? Id { get; set; }

        public string City { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Position { get; set; }

        public DateTime From { get; set; }

        public DateTime? To { get; set; }

        public int ResumeId { get; set; }

        public ICollection<DutyModel> Duties { get; set; }

        public ICollection<ProjectModel> Projects { get; set; }


        public WorkPlaceModel()
        {
            Duties = new List<DutyModel>();
            Projects = new List<ProjectModel>();
        }
    }
}
