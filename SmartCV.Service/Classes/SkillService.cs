using Microsoft.EntityFrameworkCore;
using SmartCV.Entity.Classes;
using SmartCV.Repository.Interfaces;
using SmartCV.Service.Converters;
using SmartCV.Service.Interfaces;
using SmartCV.Service.Models;
using System.Linq;

namespace SmartCV.Service.Classes
{
    public class SkillService : ISkillService
    {
        #region Declarations

        readonly ISkillRepository _skillRepository = null;
        readonly ILanguageRepository _languageRepository = null;
        readonly IResumeRepository _resumeRepository = null;

        #endregion

        public SkillService(ISkillRepository skillRepo, ILanguageRepository langRepo, IResumeRepository resumeRepo)
        {
            _skillRepository = skillRepo;
            _languageRepository = langRepo;
            _resumeRepository = resumeRepo;
        }

        public SkillLanguageAddModel Get(int managerId)
        {
            var resume = _resumeRepository
                .Get(r => r.Id == managerId && (r.Skills.Any() || r.ResumeLanguages.Any()))
                .Include(r => r.Skills)
                .Include(r => r.ResumeLanguages).ThenInclude(r => r.Language)
                .FirstOrDefault();
            if (resume == null) return null;

            var addModel = new SkillLanguageAddModel
            {
                ResumeManagerId = managerId
            };

            if (resume.Skills.Any())
            {
                foreach (var skill in resume.Skills)
                {
                    addModel.Skills.Add(skill.ToModel());
                }
            }
            if (resume.ResumeLanguages.Any())
            {
                foreach (var language in resume.ResumeLanguages)
                {
                    addModel.Languages.Add(language.Language.ToModel());
                }
            }

            return addModel;
        }

        public void CreateSkill(SkillModel model)
        {
            _skillRepository.Add(model.ToEntity());
        }

        public bool UpdateSkill(SkillModel model)
        {
            if (model.Id == null) return false;
            if (_skillRepository.Has(model.Id.Value))
            {
                var entity = model.ToEntity();
                return _skillRepository.Update(entity);
            }
            return false;
        }

        public bool UpdateLanguage(LanguageModel model)
        {
            if (model.Id == null) return false;
            if (_languageRepository.Has(model.Id.Value))
            {
                var entity = model.ToEntity();
                return _languageRepository.Update(entity);
            }
            return false;
        }

        public void CreateOrUpdate(SkillLanguageAddModel addModel)
        {
            var resume = _resumeRepository
                .Get(r => r.Id == addModel.ResumeManagerId.Value)
                .Include(r => r.ResumeLanguages)
                .FirstOrDefault();
            foreach (var skill in addModel.Skills)
            {
                skill.ResumeId = resume.Id;
                if (!this.UpdateSkill(skill))
                {
                    this.CreateSkill(skill);
                }
            }
            foreach (var language in addModel.Languages)
            {
                if (!this.UpdateLanguage(language))
                {
                    var resumeLang = new ResumeLanguage { Language = language.ToEntity() };
                    resume.ResumeLanguages.Add(resumeLang);
                }
            }

            _resumeRepository.Update(resume);
        }

        public void RemoveSkill(int id)
        {
            if (_skillRepository.Has(id)) _skillRepository.Remove(id);
        }

        public void RemoveLanguage(int id)
        {
            var resume = _resumeRepository.Get(r => r.ResumeLanguages.Any(rl => rl.Language.Id == id))
                .Include(r => r.ResumeLanguages)
                .FirstOrDefault();
            if (resume is null) return;

            var itemToRemove = resume.ResumeLanguages.FirstOrDefault(rl => rl.LanguageId == id);
            resume.ResumeLanguages.Remove(itemToRemove);
            
            _resumeRepository.Update(resume);
        }

        public void Dispose()
        {
            _skillRepository.Dispose();
            _languageRepository.Dispose();
            _resumeRepository.Dispose();
        }
    }
}
