using Entities;
using Entities.Classes;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Classes
{
    public class ResumeManagerRepository: BaseRepository<ResumeManager>, IResumeManagerRepository
    {
        private ApplicationDbContext _context = null;

        public ResumeManagerRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }



        public void Clone(int managerId)
        {
            var clone= _context.ResumeManager.Include("Resume.Education")
                                              .Include("Resume.WorkExp")
                                              .Include("Resume.Skills")
                                              .Include("Resume.Languages")
                                              .Include("Resume.PersonalQualities")
                                              .Include("Resume.CertificatesAndTrainings")
                                              .Include("Resume.Contacts")
                                              .AsNoTracking().First(e => e.Id == managerId);
            clone.Id = 0;
            clone.Resume.Id = 0;
            clone.CreatedAt = DateTime.Now;

            foreach (var institution in clone.Resume.Education)
            {
                institution.Id = 0;
            }
            foreach (var workExp in clone.Resume.WorkExp)
            {
                workExp.Id = 0;
            }
            foreach (var skill in clone.Resume.Skills)
            {
                skill.Id = 0;
            }
            foreach (var quality in clone.Resume.PersonalQualities)
            {
                quality.Id = 0;
            }
            foreach (var certificate in clone.Resume.CertificatesAndTrainings)
            {
                certificate.Id = 0;
            }
            foreach (var contact in clone.Resume.Contacts)
            {
                contact.Id = 0;
            }

            _context.ResumeManager.Add(clone);

            _context.SaveChanges();
        }
    }
}
