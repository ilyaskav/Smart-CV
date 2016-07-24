using Entities.Classes;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Converters
{
    public static class ModelExtensions
    {
        // AutoMapper - дополнение для автоматизации этого процесса

        public static WorkPlace ToEntity(this WorkPlaceModel model)
        {
            return new WorkPlace()
            {
                City = model.City,
                Name = model.Name,
                Description = model.Description,
                Position = model.Position,
                From = model.From,
                To = model.To,
                ResumeId = model.ResumeId
            };
        }

        public static WorkPlaceModel ToModel(this WorkPlace entity)
        {
            return new WorkPlaceModel()
            {
                Id = entity.Id,
                City = entity.City,
                Name = entity.Name,
                Description = entity.Description,
                Position = entity.Position,
                From = entity.From,
                To = entity.To,
                ResumeId = entity.ResumeId
            };
        }

        public static Duty ToEntity(this DutyModel model, int workPlaceId)
        {
            return new Duty()
            {
                Name = model.Name,
                WorkPlaceId = workPlaceId
            };
        }

        public static Project ToEntity(this ProjectModel model, int workPlaceId)
        {
            return new Project()
            {
                Title = model.Title,
                Duration = model.Duration,
                Description = model.Description,
                Role = model.Role,
                TechStack = model.TechStack,
                About = model.About,
                WorkPlaceId = workPlaceId
            };
        }
    }
}
