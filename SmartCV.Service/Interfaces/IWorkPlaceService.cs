using SmartCV.Service.Models;
using System;

namespace SmartCV.Service.Interfaces
{
    public interface IWorkPlaceService : IDisposable
    {
        WorkPlaceAddModel Get(int managerId);

        void CreateWorkplace(WorkPlaceModel model);

        void CreateOrUpdate(WorkPlaceAddModel addModel);

        bool UpdateWorkplace(WorkPlaceModel model);

        void RemoveDuty(int id);

        void RemoveWorkplace(int id);
    }
}
