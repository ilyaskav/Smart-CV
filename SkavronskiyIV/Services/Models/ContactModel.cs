using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Тип контакта")]
        public ContactTitleModel ContactTitle { get; set; }

        [Required]
        [DisplayName("Контакт")]
        public String Data { get; set; }

        public int? ResumeId { get; set; }

    }
}
