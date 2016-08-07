using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IResumeService : IDisposable
    {
        void CreateResume(ResumeModel model);

        void UpdateResume(ResumeModel model);

        void DeleteResume(int id);

        void CreateMSWordDocument(int id);
    }
}
