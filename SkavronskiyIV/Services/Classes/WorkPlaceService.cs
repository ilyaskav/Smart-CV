using Entities.Classes;
using Repository.Interfaces;
using Services.Converters;
using Services.Interfaces;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Classes
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

        }

        public bool UpdateWorkplace(Models.WorkPlaceModel model)
        {
            if (model.Id==null) return false;
            if (_workPlaceRepository.Has(model.Id.Value))
            {
                WorkPlace entity = model.ToEntity();

                return _workPlaceRepository.Update(entity);
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
