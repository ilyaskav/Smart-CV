using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class ProjectModel
    {
        public int? Id { get; set; }

        public String Title { get; set; }

        public String Duration { get; set; }

        public String Description { get; set; }

        public String Role { get; set; }

        public String TechStack { get; set; }

        public String About { get; set; }

        public int WorkPlaceId { get; set; }

    }
}
