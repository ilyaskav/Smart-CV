using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class ResumeManagerModel
    {
        public int? Id { get; set; }

        public int? UserId { get; set; }

        //public int? ResumeId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public int ProfessionId { get; set; }

        public ResumeManagerModel()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
