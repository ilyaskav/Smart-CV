using SmartCV.Service.Models;
using System;
using System.Collections.Generic;

namespace SmartCV.Service.Interfaces
{
    public interface IProfessionService: IDisposable
    {
        ICollection<ProfessionModel> GetAll();

        string GetRule(int managerId);
    }
}
