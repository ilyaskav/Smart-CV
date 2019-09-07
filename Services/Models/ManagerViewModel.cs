using System;

namespace Services.Models
{
    public class ManagerViewModel
    {
        public int Id { get; set; }

        public string Profession { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid Guid { get; set; }

        public bool HasLink { get; set; }
    }
}
