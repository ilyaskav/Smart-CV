using Entities.Classes;
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

        public void CreateResume(Resume model)
        {
            _resumeRepository.Add(new Resume()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                CurrentLocation = model.CurrentLocation,
                Photo = model.Photo,
                Goal = model.Goal,
                CreatedAt = DateTime.Now,
                UserId=model.UserId
            });


        }

        public void Dispose()
        {
            _resumeRepository.Dispose();
            _langRepository.Dispose();
        }
    }
}
