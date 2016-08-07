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

        public static Resume ToEntity(this ResumeModel model)
        {
            return new Resume()
            {
                Id = model.Id.Value,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                CurrentLocation = model.CurrentLocation,
                Photo = model.Photo,
                Goal = model.Goal,
                CreatedAt = DateTime.Now,
                UserId = model.UserId
            };
        }

        public static WorkPlace ToEntity(this WorkPlaceModel model)
        {
            // создаем новую работу
            var entity= new WorkPlace()
            {
                Id = model.Id.Value,
                City = model.City,
                Name = model.Name,
                Description = model.Description,
                Position = model.Position,
                From = model.From,
                To = model.To,
                ResumeId = model.ResumeId
            };

            // добавляем в нее обязанности
            foreach (var duty in model.Duties)
            {
                entity.Duties.Add(duty.ToEntity());
            }

            // добавляем в нее проекты
            foreach (var project in model.Projects)
            {
                entity.Projects.Add(project.ToEntity());
            }

            return entity;
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

        public static Duty ToEntity(this DutyModel model)
        {
            return new Duty()
            {
                Id=model.Id.Value,
                Name = model.Name,
                WorkPlaceId = model.WorkPlaceId
            };
        }

        public static Project ToEntity(this ProjectModel model)
        {
            return new Project()
            {
                Id=model.Id.Value,
                Title = model.Title,
                Duration = model.Duration,
                Description = model.Description,
                Role = model.Role,
                TechStack = model.TechStack,
                About = model.About,
                WorkPlaceId = model.WorkPlaceId
            };
        }

        public static Institution ToEntity(this InstitutionModel model)
        {
            return new Institution()
            {
                Id = model.Id.Value,
                City = model.City,
                Department = model.Department,
                Specialty = model.Specialty,
                Degree = model.Degree,
                From = model.From,
                To = model.To.Value,
                ResumeId = model.ResumeId
            };
        }

        public static Language ToEntity(this LanguageModel model)
        {
            return new Language()
            {
                Id = model.Id.Value,
                Name = model.Name,
                Level = model.Level
            };
        }

        public static Skill ToEntity(this SkillModel model)
        {
            return new Skill()
            {
                Id = model.Id.Value,
                Name = model.Name,
                ResumeId = model.ResumeId
            };
        }

        public static PersonalQuality ToEntity(this PersonalQualityModel model)
        {
            return new PersonalQuality()
            {
                Id = model.Id.Value,
                Name = model.Name,
                ResumeId = model.ResumeId
            };
        }

        public static Certificate ToEntity(this CertificateModel model)
        {
            return new Certificate()
            {
                Id = model.Id.Value,
                Name = model.Name,
                Date = model.Date,
                Location = model.Location,
                ResumeId = model.ResumeId
            };
        }
    }
}
