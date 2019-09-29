using System;

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
