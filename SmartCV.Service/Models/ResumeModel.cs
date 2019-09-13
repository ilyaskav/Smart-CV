using System;
using System.ComponentModel.DataAnnotations;

namespace SmartCV.Service.Models
{
    public class ResumeModel
    {
        public int? Id { get; set; }

        public long? UserId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public int ProfessionId { get; set; }

        public ResumeModel()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
