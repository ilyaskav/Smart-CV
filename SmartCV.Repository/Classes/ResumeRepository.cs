﻿using SmartCV.Entity.Classes;
using SmartCV.Repository.Interfaces;
using SmartCV.Entity;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SmartCV.Repository.Classes
{
    public class ResumeRepository : BaseRepository<Resume>, IResumeRepository
    {
        private readonly ApplicationDbContext _context = null;

        public ResumeRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }


        public void Clone(int managerId)
        {
            var clone = _context.Resumes
                .Include("PersonalData")
                .Include("Institutions")
                .Include("WorkPlaces.Duties")
                .Include("WorkPlaces.Projects")
                .Include("Skills")
                .Include("ResumeLanguages")
                .Include("ResumeLanguages.Language")
                .Include("PersonalQualities")
                .Include("CertificatesAndTrainings")
                .Include("Contacts")
                .AsNoTracking()
                .First(e => e.Id == managerId);

            clone.Id = 0;
            clone.PersonalData.Id = 0;
            clone.CreatedAt = DateTime.Now;
            clone.Guid = Guid.NewGuid();

            foreach (var resumeLanguages in clone.ResumeLanguages)
            {
                resumeLanguages.Id = 0;
                resumeLanguages.LanguageId = 0;
                resumeLanguages.Language.Id = 0;
            }

            foreach (var institution in clone.Institutions)
            {
                institution.Id = 0;
            }
            foreach (var workExp in clone.WorkPlaces)
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
