using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPersonalQualityService: IDisposable
    {
        void AddPersonalQuality(PersonalQualityModel model);
        void UpdatePersonalQuality(PersonalQualityModel model);
        void RemovePersonalQuality(int id);
    }
}
