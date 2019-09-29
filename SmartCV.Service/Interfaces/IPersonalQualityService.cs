using SmartCV.Service.Models;
using System;

namespace SmartCV.Service.Interfaces
{
    public interface IPersonalQualityService: IDisposable
    {
        void AddPersonalQuality(PersonalQualityModel model);
        void UpdatePersonalQuality(PersonalQualityModel model);
        void RemovePersonalQuality(int id);
    }
}
