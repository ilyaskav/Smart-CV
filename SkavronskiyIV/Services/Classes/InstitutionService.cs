using Repository.Interfaces;
using Services.Interfaces;
using Services.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Models;

namespace Services.Classes
{
    public class InstitutionService : IInstitutionService
    {
        #region Declarations

        IInstitutionRepository _institutionRepository = null;
        IResumeRepository _resumeRepository = null;
        IResumeManagerRepository _managerRepository = null;

        #endregion

        public InstitutionService(IInstitutionRepository instRepo, IResumeRepository resumeRepo, IResumeManagerRepository managerRepo)
        {
            _institutionRepository = instRepo;
            _resumeRepository = resumeRepo;
            _managerRepository = managerRepo;
        }

        public void Dispose()
        {
            _institutionRepository.Dispose();
            _resumeRepository.Dispose();
            _managerRepository.Dispose();
        }

        public InstitutionAddModel Get(int managerId)
        {
            var institutions = _managerRepository.Get(managerId).Resume.Education;
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
