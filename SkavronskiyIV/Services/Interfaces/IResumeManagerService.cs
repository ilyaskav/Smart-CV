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
        int CreateEmptyResume(ResumeManagerModel model);

        void CopyResume(int managerId);

        ICollection<ManagerViewModel> GetAllResumes(int userId);

        bool IsOwnedBy(int userId, int managerId);

        void DeleteResume(int id);
    }
}
