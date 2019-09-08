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
        private readonly IResumeManagerRepository _managerRepository = null;

        #endregion

        public WorkPlaceService(IWorkPlaceRepository workRepo, IDutyRepository dutyRepo, IProjectRepository projRepo, IResumeRepository resumeRepo, IResumeManagerRepository managerRepo)
        {
            _workPlaceRepository = workRepo;
            _dutyRepository = dutyRepo;
            _projectRepository = projRepo;
            _resumeRepository = resumeRepo;
            _managerRepository = managerRepo;
        }

        public void Dispose()
        {
            _workPlaceRepository.Dispose();
            _dutyRepository.Dispose();
            _projectRepository.Dispose();
            _resumeRepository.Dispose();
            _managerRepository.Dispose();
        }

        public WorkPlaceAddModel Get(int managerId)
        {
            var resume=_resumeRepository.Get().FirstOrDefault(r=>r.ResumeManager.Id == managerId);
            if (resume == null || resume.WorkExp.Count == 0) return null;

            WorkPlaceAddModel addModel = new WorkPlaceAddModel();
            addModel.ResumeManagerId = managerId;
            foreach (var work in resume.WorkExp)
            {
                addModel.WorkPlaces.Add(work.ToModel());
            }

            return addModel;
        }

        public void CreateWorkplace(Models.WorkPlaceModel model)
        {
            // создаем новую работу
            _workPlaceRepository.Add(model.ToEntity());
        }

        public void CreateOrUpdate(WorkPlaceAddModel addModel)
        {
            var resume = _managerRepository.Get(addModel.ResumeManagerId.Value).Resume;
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
            if (model.Id==null) return false;
            if (_workPlaceRepository.Has(model.Id.Value))
            {
                WorkPlace entity = model.ToEntity();

                _workPlaceRepository.Update(entity);
                foreach (var duty in entity.Duties)
                {
                    _dutyRepository.Update(duty);
                }
                foreach (var proj in entity.Projects)
                {
                    _projectRepository.Update(proj);
                }
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
