using SmartCV.Service.Models;
using System;

namespace SmartCV.Service.Interfaces
{
    public interface IResumeService : IDisposable
    {
        void CreateResume(ResumeModel model);

        void UpdateResume(ResumeModel model);

        ResumeModel GetResume(int id);

        ResumeModel GetResumeByManagerId(int id);

        void DeleteResume(int id);

        void CreateMSWordDocument(Guid id);

        void CreatePDF(Guid id);
    }
}
