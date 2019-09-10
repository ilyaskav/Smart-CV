﻿using SmartCV.Repository.Interfaces;
using SmartCV.Service.Converters;
using SmartCV.Service.Interfaces;
using SmartCV.Service.Models;
using System.Linq;

namespace SmartCV.Service.Classes
{
    public class SkillService: ISkillService
    {
        #region Declarations

        ISkillRepository _skillRepository = null;
        ILanguageRepository _languageRepository = null;
        IResumeManagerRepository _managerRepository = null;

        #endregion

        public SkillService(ISkillRepository skillRepo, ILanguageRepository langRepo, IResumeManagerRepository managerRepo)
        {
            _skillRepository = skillRepo;
            _languageRepository = langRepo;
            _managerRepository = managerRepo;
        }

        public SkillLanguageAddModel Get(int managerId)
        {
            var resume = _managerRepository.Get(managerId).Resume;
            if (resume == null || (resume.Skills.Count == 0 && resume.ResumeLanguages.Count == 0)) return null;

            SkillLanguageAddModel addModel = new SkillLanguageAddModel();
            addModel.ResumeManagerId = managerId;
            if (resume.Skills.Count>0)
            {
                foreach (var skill in resume.Skills)
                {
                    addModel.Skills.Add(skill.ToModel());
                }
            }
            if (resume.ResumeLanguages.Count > 0)
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

        public void CreateLanguage(LanguageModel model)
        {
            _languageRepository.Add(model.ToEntity());
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
            var resume = _managerRepository.Get(addModel.ResumeManagerId.Value).Resume;
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
                    this.CreateLanguage(language);
                }
            }
        }

        public void RemoveSkill(int id)
        {
            if (_skillRepository.Has(id)) _skillRepository.Remove(id);
        }

        public void Dispose()
        {
            _skillRepository.Dispose();
            _languageRepository.Dispose();
        }
    }
}