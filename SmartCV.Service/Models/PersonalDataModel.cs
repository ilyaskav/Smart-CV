using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace SmartCV.Service.Models
{
    public class PersonalDataModel
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [DisplayName("Имя")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [DisplayName("Фамилия")]
        public string LastName { get; set; }

        //[Required]
        //[DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Дата рождения")]
        // System.Globalization.Culture
        public DateTime? DateOfBirth { get; set; }

        public string CurrentLocation { get; set; }

        // path to img
        [StringLength(100, MinimumLength = 8)]
        public string Photo { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10)]
        [DisplayName("Цель")]
        public string Goal { get; set; }

        //[Required]
        public int ResumeId { get; set; }

    }
}
