using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCV.Service.Models
{
    public class ResumeManagerModel
    {
        public int? Id { get; set; }

        public long? UserId { get; set; }

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
