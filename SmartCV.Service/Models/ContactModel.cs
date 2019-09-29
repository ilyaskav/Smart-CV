using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartCV.Service.Models
{
    public class ContactModel
    {
        public int? Id { get; set; }

        [Required]
        [DisplayName("Тип контакта")]
        public ContactTitleModel ContactTitle { get; set; }

        [Required]
        [DisplayName("Контакт")]
        public string Data { get; set; }

        public int? ResumeId { get; set; }

    }
}
