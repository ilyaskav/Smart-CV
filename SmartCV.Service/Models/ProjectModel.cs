using System;

namespace SmartCV.Service.Models
{
    public class ProjectModel
    {
        public int? Id { get; set; }

        public string Title { get; set; }

        public string Duration { get; set; }

        public string Description { get; set; }

        public string Role { get; set; }

        public string TechStack { get; set; }

        public string About { get; set; }

        public int WorkPlaceId { get; set; }

    }
}
