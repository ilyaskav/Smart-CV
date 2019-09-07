using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
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
