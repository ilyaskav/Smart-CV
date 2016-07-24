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
            int workPlaceId = _workPlaceRepository.Add(model.ToEntity());

            // добавляем в нее обязанности
            foreach (var duty in model.Duties)
            {
                _dutyRepository.Add(duty.ToEntity(workPlaceId));
            }

            // добавляем в нее проекты
            foreach (var project in model.Projects)
            {
                _projectRepository.Add(project.ToEntity(workPlaceId));
            }
        }

        public void UpdateWorkplace(Models.WorkPlaceModel model)
        {
            if (model.Id==null) return;
            if (_workPlaceRepository.Has(model.Id.Value))
            {
                //var entity = _workPlaceRepository.Get(model.Id.Value);
                //entity.City = model.City;
                //entity.Description = model.Description;
                //entity.Name = model.Name;
                //entity.Position=model.Position;
                //entity.From=model.From;
                //entity.To = model.To;
                //Доделать!!!!!
                WorkPlace entity = model.ToEntity();
                foreach (var duty in model.Duties)
                {

                }

                _workPlaceRepository.Update(entity);
            }
        }

        public void RemoveWorkplace(int id)
        {
            if (_workPlaceRepository.Has(id)) _workPlaceRepository.Remove(id);
        }
    }
}
