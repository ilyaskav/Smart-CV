﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

 
namespace Services.Models
{
    public class ResumeModel
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength=3)]
        [DisplayName("Имя")]
        public String FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [DisplayName("Фамилия")]
        public String LastName { get; set; }

        //[Required]
        //[DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        //[DisplayName("Дата рождения")]
        // System.Globalization.Culture
        public DateTime DateOfBirth { get; set; }

        public String CurrentLocation { get; set; }

        // path to img
        [StringLength(100, MinimumLength = 8)]
        public String Photo { get; set; }

        [Required]
        [StringLength(100, MinimumLength=10)]
        [DisplayName("Цель")]
        public String Goal { get; set; }

        //[Required]
        public int ManagerId { get; set; }

    }
}
