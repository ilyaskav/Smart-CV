using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class CertificateModel
    {
        public int? Id { get; set; }

        [Required]
        public String Name { get; set; }

        public DateTime Date { get; set; }

        public String Location { get; set; }

        public int? ResumeId { get; set; }

    }
}
