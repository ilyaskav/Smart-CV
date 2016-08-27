using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class ContactModel
    {
        public int? Id { get; set; }

        [Required]
        public ContactTitleModel ContactTitle { get; set; }

        [Required]
        public String Data { get; set; }

        public int? ResumeId { get; set; }

    }
}
