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
            if (model == null)
            {
                throw new NullReferenceException();
            }

            return new Resume()
            {
                Id = model.Id.HasValue ? model.Id.Value : 0,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                CurrentLocation = model.CurrentLocation,
                Photo = model.Photo,
                Goal = model.Goal
            };
        }

        public static ResumeModel ToModel(this Resume entity)
        {
            if (entity == null)
            {
                throw new NullReferenceException();
            }

            return new ResumeModel()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                DateOfBirth = entity.DateOfBirth,
                CurrentLocation = entity.CurrentLocation,
                Photo = entity.Photo,
                Goal = entity.Goal
            };
        }

        public static ResumeManager ToEntity(this ResumeManagerModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException("ResumeManagerModel is null");
            }
            return new ResumeManager()
            {
                Id= model.Id.HasValue ? model.Id.Value : 0,
                CreatedAt=model.CreatedAt,
                ProfessionId= model.ProfessionId,
                UserId= model.UserId.HasValue ? model.UserId.Value : 0
            };
        }

        public static ResumeManagerModel ToModel(this ResumeManager entity)
        {
            if (entity == null)
            {
                throw new NullReferenceException("ResumeManager is null");
            }

            return new ResumeManagerModel(){
                Id= entity.Id,
                CreatedAt=entity.CreatedAt,
                ProfessionId = entity.ProfessionId,
                UserId = entity.UserId
            };
        }

        public static ProfessionModel ToModel(this Profession entity)
        {
            if (entity == null)
            {
                throw new NullReferenceException("Profession is null");
            }

            return new ProfessionModel()
            {
                Id=entity.Id,
                Name=entity.Name,
                Rule=entity.Rules
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
            if (model == null)
            {
                throw new NullReferenceException("InstitutionModel is null");
            }
            return new Institution()
            {
                Id = model.Id.HasValue? model.Id.Value: 0,
                Name=model.Name,
                City = model.City,
                Department = model.Department,
                Specialty = model.Specialty,
                Degree = model.Degree,
                From = model.From,
                To = model.To,
                ResumeId = model.ResumeId.HasValue ? model.ResumeId.Value : 0
            };
        }

        public static InstitutionModel ToModel(this Institution entity){
            if (entity == null)
            {
                throw new NullReferenceException("Institution is null");
            }

            return new InstitutionModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                City = entity.City,
                Department = entity.Department,
                Specialty = entity.Specialty,
                Degree = entity.Degree,
                From = entity.From,
                To = entity.To,
                ResumeId = entity.ResumeId
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

        public static ContactTitleModel ToModel(this ContactTitle entity){
            if (entity == null)
            {
                throw new NullReferenceException("ContactTitle is null");
            }

            return new ContactTitleModel(){
                Id=entity.Id,
                Title=entity.Title
            };
        }

        public static ContactTitle ToEntity(this ContactTitleModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException("ContactTitleModel is null");
            }

            return new ContactTitle()
            {
                Id = model.Id.HasValue ? model.Id.Value : 0,
                Title = model.Title
            };
        }

        public static ContactModel ToModel(this Contact entity)
        {
            if (entity == null)
            {
                throw new NullReferenceException("Contact is null");
            }

            return new ContactModel()
            {
                Id = entity.Id,
                ContactTitle = entity.ContactTitle.ToModel(),
                Data = entity.Data,
                ResumeId=entity.ResumeId
            };
        }

        public static Contact ToEntity(this ContactModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException("ContactModel is null");
            }

            return new Contact()
            {
                Id = model.Id.HasValue ? model.Id.Value : 0,
                ContactTitleId = model.ContactTitle.Id.HasValue ? model.ContactTitle.Id.Value : 0,
                Data = model.Data,
                ResumeId = model.ResumeId.HasValue ? model.ResumeId.Value : 0
            };
        }

        public static ContactAddModel ToAddModel(this ICollection<Contact> entity)
        {
            string email=string.Empty, phone=string.Empty;
            ICollection<ContactModel> additionalContacts = new List<ContactModel>();

            foreach (var contact in entity)
            {
                switch (contact.ContactTitle.Title)
                {
                    case "EMail":
                        email = contact.Data;
                        break;
                    case "Phone":
                        phone = contact.Data;
                        break;
                    default:
                        additionalContacts.Add(contact.ToModel());
                        break;
                }
            }
            return new ContactAddModel()
            {
                EMail = email,
                Phone = phone,
                Contacts = additionalContacts
            };
        }
    }
}
