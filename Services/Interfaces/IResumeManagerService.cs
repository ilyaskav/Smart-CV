using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IResumeManagerService: IDisposable
    {
        ResumeManagerPrintModel Get(int managerId);

        ResumeManagerPrintModel Get(Guid identifier);

        int CreateEmptyResume(ResumeManagerModel model);

        void CopyResume(int managerId);

        ICollection<ManagerViewModel> GetAllResumes(int userId);

        bool IsOwnedBy(int userId, int managerId);

        bool IsOwnedBy(int userId, Guid identifier);

        void DeleteResume(int id);
    }
}
