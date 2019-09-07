using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ILanguageService:IDisposable
    {
        void AddLanguage(LanguageModel model);
        void UpdateLanguage(LanguageModel model);
        void DeleteLanguage(int id);
    }
}
