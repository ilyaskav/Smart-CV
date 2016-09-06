using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class SkillModel
    {
        public int? Id { get; set; }

        public int? ResumeId { get; set; }

        [Required]
        public String Name { get; set; }

    }
}
