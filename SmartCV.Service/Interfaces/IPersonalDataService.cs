using SmartCV.Service.Models;
using System;

namespace SmartCV.Service.Interfaces
{
    public interface IPersonalDataService : IDisposable
    {
        void CreateResume(PersonalDataModel model);

        void UpdateResume(PersonalDataModel model);

        PersonalDataModel GetResume(int id);

        PersonalDataModel GetPersonalDataByResumeId(int id);

        void DeleteResume(int id);

        void CreateMSWordDocument(Guid id);

        void CreatePDF(Guid id);
    }
}
