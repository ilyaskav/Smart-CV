using Microsoft.EntityFrameworkCore;
using SmartCV.Entity.Classes;
using SmartCV.Repository.Interfaces;
using SmartCV.Service.Converters;
using SmartCV.Service.Interfaces;
using SmartCV.Service.Models;
using System.Linq;

namespace SmartCV.Service.Classes
{
    public class WorkPlaceService : IWorkPlaceService
    {
        #region Declarations

        private readonly IWorkPlaceRepository _workPlaceRepository = null;
        private readonly IDutyRepository _dutyRepository = null;
        private readonly IProjectRepository _projectRepository = null;
        private readonly IResumeRepository _resumeRepository = null;

        #endregion

        public WorkPlaceService(IWorkPlaceRepository workRepo, IDutyRepository dutyRepo, IProjectRepository projRepo, IResumeRepository resumeRepo)
        {
            _workPlaceRepository = workRepo;
            _dutyRepository = dutyRepo;
            _projectRepository = projRepo;
            _resumeRepository = resumeRepo;
        }

        public void Dispose()
        {
            _workPlaceRepository.Dispose();
            _dutyRepository.Dispose();
            _projectRepository.Dispose();
            _resumeRepository.Dispose();
        }

        public WorkPlaceAddModel Get(int managerId)
        {
            var resume = _resumeRepository
                .Get(r => r.Id == managerId)
                .Include(r => r.WorkExp)
                    .ThenInclude(r => r.Duties)
                .FirstOrDefault();
            if (resume == null || !resume.WorkExp.Any()) return null;

            var addModel = new WorkPlaceAddModel
            {
                ResumeManagerId = managerId
            };
            foreach (var work in resume.WorkExp)
            {
                addModel.WorkPlaces.Add(work.ToModel());
            }

            return addModel;
        }

        public void CreateWorkplace(WorkPlaceModel model)
        {
            _workPlaceRepository.Add(model.ToEntity());
        }

        public void CreateOrUpdate(WorkPlaceAddModel addModel)
        {
            var resume = _resumeRepository.Get(addModel.ResumeManagerId.Value);
            foreach (var work in addModel.WorkPlaces)
            {
                work.ResumeId = resume.Id;
                if (!this.UpdateWorkplace(work))
                {
                    this.CreateWorkplace(work);
                }
            }
        }

        public bool UpdateWorkplace(WorkPlaceModel model)
        {
            if (model.Id == null) return false;
            if (_workPlaceRepository.Has(model.Id.Value))
            {
                WorkPlace entity = model.ToEntity();

                foreach (var duty in entity.Duties)
                {
                    _dutyRepository.Upsert(duty);
                }
                foreach (var proj in entity.Projects)
                {
                    _projectRepository.Update(proj);
                }
                _workPlaceRepository.Update(entity);

                return true;
            }
            return false;
        }

        public void RemoveDuty(int id)
        {
            _dutyRepository.Remove(id);
        }

        public void RemoveWorkplace(int id)
        {
            if (_workPlaceRepository.Has(id)) _workPlaceRepository.Remove(id);
        }
    }
}
