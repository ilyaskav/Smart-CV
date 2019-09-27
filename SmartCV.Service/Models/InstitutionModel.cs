using System;
using System.ComponentModel.DataAnnotations;

namespace SmartCV.Service.Models
{
    public class InstitutionModel
    {
        public int? Id { get; set; }

        public string City { get; set; }

        [Required]
        public string Name { get; set; }

        public string Department { get; set; }

        [Required]
        public string Specialty { get; set; }

        [Required]
        public string Degree { get; set; }

        [Required]
        public DateTime From { get; set; }

        public DateTime? To { get; set; }

        public int? ResumeId { get; set; }

    }
}
