using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class InstitutionModel
    {
        public int? Id { get; set; }

        public String City { get; set; }

        public String Name { get; set; }

        public String Department { get; set; }

        public String Specialty { get; set; }

        public String Degree { get; set; }

        public DateTime From { get; set; }

        public DateTime? To { get; set; }

        public int ResumeId { get; set; }

    }
}
