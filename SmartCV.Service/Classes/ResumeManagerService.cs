using SmartCV.Repository.Interfaces;
using SmartCV.Service.Converters;
using SmartCV.Service.Interfaces;
using SmartCV.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartCV.Service.Classes
{
    public class ResumeManagerService : IResumeManagerService
    {
        #region declarations

        IResumeManagerRepository _resumeManagerRepository = null;
        IResumeRepository _resumeRepository = null;

        #endregion

        public ResumeManagerService(IResumeManagerRepository resumeManRepo, IResumeRepository resumeRepo)
        {
            _resumeManagerRepository = resumeManRepo;
            _resumeRepository = resumeRepo;
        }

        public ResumeManagerPrintModel Get(int managerId)
        {
            return _resumeManagerRepository.Get(managerId).ToPrintModel();
        }

        public ResumeManagerPrintModel Get(Guid identifier)
        {
            var manager = _resumeManagerRepository.Get(m => m.Guid.Equals(identifier)).FirstOrDefault();

            if (manager == null) return null;
            return manager.ToPrintModel();
        }

        public int CreateEmptyResume(Models.ResumeManagerModel model)
        {
            return _resumeManagerRepository.Add(model.ToEntity());
        }

        public void CopyResume(int managerId)
        {
            if (!_resumeManagerRepository.Has(managerId)) return;

            _resumeManagerRepository.Clone(managerId);
        }

        public ICollection<ManagerViewModel> GetAllResumes(long userId)
        {
            var entities = _resumeManagerRepository.Get().Where(user => user.UserId.Equals(userId)).OrderByDescending(e => e.CreatedAt).ToList();
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
            if (_resumeManagerRepository.Has(id))
            {
                var manager = _resumeManagerRepository.Get(id);

                if (manager.Resume != null)
                {
                    _resumeRepository.Remove(manager.Resume.Id);
                }
                _resumeManagerRepository.Remove(id);
            }
        }

        public void Dispose()
        {
            _resumeManagerRepository.Dispose();
        }


        public bool IsOwnedBy(long userId, int managerId)
        {
            var resumeManager = _resumeManagerRepository.Get(managerId);

            if (resumeManager.UserId == userId) return true;
            else return false;
        }

        public bool IsOwnedBy(long userId, Guid identifier)
        {
            var manager = _resumeManagerRepository.Get(m => m.Guid.Equals(identifier)).FirstOrDefault();

            if (manager.UserId == userId) return true;
            else return false;
        }
    }
}
