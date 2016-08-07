using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IInstitutionService : IDisposable
    {
        void CreateInstitution(InstitutionModel model);
        void UpdateInstitution(InstitutionModel model);
        void DeleteInstitution(int id);
    }
}
