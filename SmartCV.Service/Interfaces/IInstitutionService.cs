using SmartCV.Service.Models;
using System;

namespace SmartCV.Service.Interfaces
{
    public interface IInstitutionService : IDisposable
    {
        void CreateInstitution(InstitutionModel model);
        void CreateOrUpdate(InstitutionAddModel addModel);
        bool UpdateInstitution(InstitutionModel model);
        InstitutionAddModel Get(int resumeManager);
        void RemoveInstitution(int id);
    }
}
