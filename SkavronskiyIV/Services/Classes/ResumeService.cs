using Repository.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Classes
{
    public class ResumeService : IResumeService
    {
        #region Declarations

        private readonly IResumeRepository _resumeRepository = null;
        private readonly ILanguageRepository _langRepository = null;

        #endregion

        public ResumeService(IResumeRepository resumeRepository, ILanguageRepository langRepository)
        {
            _resumeRepository = resumeRepository;
            _langRepository = langRepository;
        }

        public void CreateResume()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _resumeRepository.Dispose();
            _langRepository.Dispose();
        }
    }
}
