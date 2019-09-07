using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class InstitutionModel
    {
        public int? Id { get; set; }

        public String City { get; set; }

        [Required]
        public String Name { get; set; }

        public String Department { get; set; }

        [Required]
        public String Specialty { get; set; }

        [Required]
        public String Degree { get; set; }

        [Required]
        public DateTime From { get; set; }

        public DateTime? To { get; set; }

        public int? ResumeId { get; set; }

    }
}
