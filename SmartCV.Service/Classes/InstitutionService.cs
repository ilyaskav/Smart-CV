using Microsoft.EntityFrameworkCore;
using SmartCV.Repository.Interfaces;
using SmartCV.Service.Converters;
using SmartCV.Service.Interfaces;
using SmartCV.Service.Models;
using System.Collections.Generic;
using System.Linq;

namespace SmartCV.Service.Classes
{
    public class InstitutionService : IInstitutionService
    {
        #region Declarations

        readonly IInstitutionRepository _institutionRepository = null;
        readonly IPersonalDataRepository _personalDataRepository = null;
        readonly IResumeRepository _resumeRepository = null;

        #endregion

        public InstitutionService(IInstitutionRepository instRepo, IPersonalDataRepository personalDataRepo, IResumeRepository resumeRepo)
        {
            _institutionRepository = instRepo;
            _personalDataRepository = personalDataRepo;
            _resumeRepository = resumeRepo;
        }

        public void Dispose()
        {
            _institutionRepository.Dispose();
            _personalDataRepository.Dispose();
            _resumeRepository.Dispose();
        }

        public InstitutionAddModel Get(int managerId)
        {
            var resume = _resumeRepository.Get(r => r.Id == managerId && r.PersonalData != null).Include(r => r.Education).FirstOrDefault();
            if (resume == null) return null;

            var institutions = resume.Education;
            var addModel = new InstitutionAddModel() { ResumeManagerId = managerId };

            foreach (var inst in institutions)
            {
                addModel.Institutions.Add(inst.ToModel());
            }
            return addModel;
        }

        public void CreateInstitution(InstitutionModel model)
        {
            _institutionRepository.Add(model.ToEntity());
        }

        public void CreateOrUpdate(InstitutionAddModel addModel)
        {
            var resume = _resumeRepository.Get(addModel.ResumeManagerId);
            foreach (var institution in addModel.Institutions)
            {
                institution.ResumeId = resume.Id;
                if (!this.UpdateInstitution(institution))
                {
                    this.CreateInstitution(institution);
                }
            }
        }

        public bool UpdateInstitution(InstitutionModel model)
        {
            if (model.Id == null) return false;
            if (_institutionRepository.Has(model.Id.Value))
            {
                var entity = model.ToEntity();
                return _institutionRepository.Update(entity);
            }
            return false;
        }

        public void RemoveInstitution(int id)
        {
            if (_institutionRepository.Has(id)) _institutionRepository.Remove(id);
        }

    }
}
