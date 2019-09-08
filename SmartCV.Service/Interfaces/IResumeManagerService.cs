using SmartCV.Service.Models;
using System;
using System.Collections.Generic;

namespace SmartCV.Service.Interfaces
{
    public interface IResumeManagerService: IDisposable
    {
        ResumeManagerPrintModel Get(int managerId);

        ResumeManagerPrintModel Get(Guid identifier);

        int CreateEmptyResume(ResumeManagerModel model);

        void CopyResume(int managerId);

        ICollection<ManagerViewModel> GetAllResumes(long userId);

        bool IsOwnedBy(long userId, int managerId);

        bool IsOwnedBy(long userId, Guid identifier);

        void DeleteResume(int id);
    }
}
