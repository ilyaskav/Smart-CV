using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IProfessionService: IDisposable
    {
        ICollection<ProfessionModel> GetAll();

        string GetRule(int managerId);
    }
}
