﻿using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IWorkPlaceService : IDisposable
    {
        void CreateWorkplace(WorkPlaceModel model);
        void UpdateWorkplace(WorkPlaceModel model);
        void RemoveWorkplace(int id);
    }
}
