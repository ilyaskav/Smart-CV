using Entities.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Resume> Resumes { get; set; }

        public DbSet<WorkPlace> Workplaces { get; set; }

        public DbSet<Institution> Institutions { get; set; }

        public DbSet<Skill> Skills { get; set; }

        public DbSet<Certificate> Certificates { get; set; }

        public DbSet<Duty> Duties { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<PersonalQuality> PersonalQualities { get; set; }

        public DbSet<ContactTitle> ContactTitles { get; set; }
    }
}
