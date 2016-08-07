using Entities.Classes;
using Repository.Interfaces;
using Services.Converters;
using Services.Interfaces;
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

        #endregion

        public WorkPlaceService(IWorkPlaceRepository workRepo, IDutyRepository dutyRepo, IProjectRepository projRepo)
        {
            _workPlaceRepository = workRepo;
            _dutyRepository = dutyRepo;
            _projectRepository = projRepo;
        }


        public void Dispose()
        {
            _workPlaceRepository.Dispose();
            _dutyRepository.Dispose();
            _projectRepository.Dispose();
        }


        public void CreateWorkplace(Models.WorkPlaceModel model)
        {
            // создаем новую работу
            _workPlaceRepository.Add(model.ToEntity());

            //var entity = _workPlaceRepository.Get(workPlaceId);

            //// добавляем в нее обязанности
            //foreach (var duty in model.Duties)
            //{
            //    entity.Duties.Add(duty.ToEntity());
            //    //_dutyRepository.Add(duty.ToEntity());
            //}

            //// добавляем в нее проекты
            //foreach (var project in model.Projects)
            //{
            //    entity.Projects.Add(project.ToEntity());
            //    //_projectRepository.Add(project.ToEntity());
            //}
        }

        public void UpdateWorkplace(Models.WorkPlaceModel model)
        {
            if (model.Id==null) return;
            if (_workPlaceRepository.Has(model.Id.Value))
            {
                WorkPlace entity = model.ToEntity();

                _workPlaceRepository.Update(entity);
            }
        }

        public void RemoveWorkplace(int id)
        {
            if (_workPlaceRepository.Has(id)) _workPlaceRepository.Remove(id);
        }
    }
}
