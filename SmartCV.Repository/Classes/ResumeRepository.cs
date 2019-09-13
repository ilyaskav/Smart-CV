using SmartCV.Entity.Classes;
using SmartCV.Repository.Interfaces;
using SmartCV.Entity;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SmartCV.Repository.Classes
{
    public class ResumeRepository : BaseRepository<Resume>, IResumeRepository
    {
        private ApplicationDbContext _context = null;

        public ResumeRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }


        public void Clone(int managerId)
        {
            var clone = _context.Resumes
                .Include("Institutions")
                .Include("WorkExp.Duties")
                .Include("WorkExp.Projects")
                .Include("Skills")
                .Include("Languages")
                .Include("PersonalQualities")
                .Include("CertificatesAndTrainings")
                .Include("Contacts")
                .AsNoTracking()
                .First(e => e.Id == managerId);

            clone.Id = 0;
            clone.PersonalData.Id = 0;
            clone.CreatedAt = DateTime.Now;
            clone.Guid = Guid.NewGuid();

            foreach (var institution in clone.Education)
            {
                institution.Id = 0;
            }
            foreach (var workExp in clone.WorkExp)
            {
                workExp.Id = 0;
                foreach (var duty in workExp.Duties)
                {
                    duty.Id = 0;
                    duty.WorkPlaceId = 0;
                }

                foreach (var proj in workExp.Projects)
                {
                    proj.Id = 0;
                    proj.WorkPlaceId = 0;
                }
            }

            foreach (var skill in clone.Skills)
            {
                skill.Id = 0;
            }
            foreach (var quality in clone.PersonalQualities)
            {
                quality.Id = 0;
            }
            foreach (var certificate in clone.CertificatesAndTrainings)
            {
                certificate.Id = 0;
            }
            foreach (var contact in clone.Contacts)
            {
                contact.Id = 0;
            }

            _context.Resumes.Add(clone);

            _context.SaveChanges();
        }
    }
}
