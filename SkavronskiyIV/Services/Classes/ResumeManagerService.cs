using Repository.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Converters;

namespace Services.Classes
{
    public class ResumeManagerService : IResumeManagerService
    {
        #region declarations

        IResumeManagerRepository _resumeManagerRepository = null;
        #endregion

        public ResumeManagerService(IResumeManagerRepository resumeManRepo)
        {
            _resumeManagerRepository = resumeManRepo;
        }

        public int CreateEmptyResume(Models.ResumeManagerModel model)
        {
            return _resumeManagerRepository.Add(model.ToEntity());
        }

        public ICollection<Models.ResumeManagerModel> GetAllResumes(int userId)
        {
            var entities = _resumeManagerRepository.Get().Where(user => user.UserId.Equals(userId)).ToList();
            ICollection<Models.ResumeManagerModel> models = new List<Models.ResumeManagerModel>();
            foreach (var entity in entities)
            {
                models.Add(entity.ToModel());
            }
            return models;

        }

        public void DeleteResume(int id)
        {
            if (_resumeManagerRepository.Has(id)) _resumeManagerRepository.Remove(id);
        }

        public void Dispose()
        {
            _resumeManagerRepository.Dispose();
        }


        public bool IsOwnedBy(int userId, int managerId)
        {
            var resumeManager=_resumeManagerRepository.Get(managerId);

            if (resumeManager.UserId == userId) return true;
            else return false;
        }
    }
}
