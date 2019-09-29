using SmartCV.Repository.Interfaces;
using SmartCV.Service.Converters;
using SmartCV.Service.Interfaces;
using SmartCV.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartCV.Service.Classes
{
    public class ResumeService : IResumeService
    {
        #region declarations

        readonly IResumeRepository _resumeRepository = null;
        readonly IPersonalDataRepository _personalDataRepository = null;

        #endregion

        public ResumeService(IResumeRepository resumeRepo, IPersonalDataRepository personalDataRepo)
        {
            _resumeRepository = resumeRepo;
            _personalDataRepository = personalDataRepo;
        }

        public ResumeManagerPrintModel Get(int managerId)
        {
            return _resumeRepository.Get(managerId).ToPrintModel();
        }

        public ResumeManagerPrintModel Get(Guid identifier)
        {
            var manager = _resumeRepository.Get(m => m.Guid.Equals(identifier)).FirstOrDefault();

            if (manager == null) return null;
            return manager.ToPrintModel();
        }

        public int CreateEmptyResume(Models.ResumeModel model)
        {
            return _resumeRepository.Add(model.ToEntity());
        }

        public void CopyResume(int managerId)
        {
            if (!_resumeRepository.Has(managerId)) return;

            _resumeRepository.Clone(managerId);
        }

        public ICollection<ManagerViewModel> GetAllResumes(long userId)
        {
            var entities = _resumeRepository.Get().Where(user => user.UserId.Equals(userId)).OrderByDescending(e => e.CreatedAt).ToList();
            ICollection<ManagerViewModel> models = new List<ManagerViewModel>();

            foreach (var entity in entities)
            {
                models.Add(new ManagerViewModel()
                {
                    Id = entity.Id,
                    CreatedAt = entity.CreatedAt,
                    Profession = entity.Profession.Name,
                    Guid = entity.Guid,
                    HasLink = entity.Link != null
                });
            }
            return models;

        }

        public void DeleteResume(int id)
        {
            if (_resumeRepository.Has(id))
            {
                var resume = _resumeRepository.Get(id);

                if (resume != null)
                {
                    _personalDataRepository.Remove(resume.Id);
                }
                _resumeRepository.Remove(id);
            }
        }

        public void Dispose()
        {
            _resumeRepository.Dispose();
            _personalDataRepository.Dispose();
        }


        public bool IsOwnedBy(long userId, int managerId)
        {
            var resumeManager = _resumeRepository.Get(managerId);

            if (resumeManager.UserId == userId) return true;
            else return false;
        }

        public bool IsOwnedBy(long userId, Guid identifier)
        {
            var manager = _resumeRepository.Get(m => m.Guid.Equals(identifier)).FirstOrDefault();

            if (manager.UserId == userId) return true;
            else return false;
        }
    }
}
