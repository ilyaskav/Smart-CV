using System;
using System.ComponentModel.DataAnnotations;

namespace SmartCV.Service.Models
{
    public class SkillModel
    {
        public int? Id { get; set; }

        public int? ResumeId { get; set; }

        [Required]
        public string Name { get; set; }

    }
}
