using System;
using System.ComponentModel.DataAnnotations;

namespace SmartCV.Service.Models
{
    public class CertificateModel
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Location { get; set; }

        public int? ResumeId { get; set; }

    }
}
