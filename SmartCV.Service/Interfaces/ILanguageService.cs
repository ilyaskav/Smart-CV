using SmartCV.Service.Models;
using System;

namespace SmartCV.Service.Interfaces
{
    public interface ILanguageService:IDisposable
    {
        void AddLanguage(LanguageModel model);
        void UpdateLanguage(LanguageModel model);
        void DeleteLanguage(int id);
    }
}
