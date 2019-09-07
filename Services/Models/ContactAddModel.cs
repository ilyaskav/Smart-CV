﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class ContactAddModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Электронная почта")]
        public string EMail { get; set; }

        [Required]
        [DisplayName("Контактный телефон")]
        public string Phone { get; set; }

        public int? ResumeManagerId { get; set; }

        [UIHint("Contacts")]
        public ICollection<ContactModel> Contacts { get; set; }

        public ContactAddModel()
        {
            Contacts = new List<ContactModel>();
        }
    }
}