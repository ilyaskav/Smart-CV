using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCV.Service.Models
{
    public class ResumeManagerPrintModel
    {
        public int Id { get; set; }

        public long UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Link { get; set; }

        public Guid Guid { get; set; }
    }
}
