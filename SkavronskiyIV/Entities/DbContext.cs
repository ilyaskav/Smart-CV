using Entities.Classes;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public DbSet<Resume> Resumes { get; set; }

        public DbSet<WorkPlace> Workplaces { get; set; }

        public DbSet<Institution> Institutions { get; set; }

        public DbSet<Skill> Skills { get; set; }

        public DbSet<Certificate> Certificates { get; set; }

        public DbSet<Duty> Duties { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<PersonalQuality> PersonalQualities { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<ContactTitle> ContactTitles { get; set; }

        public DbSet<ResumeManager> ResumeManager { get; set; }

        public DbSet<Profession> Professions { get; set; }


        public ApplicationDbContext()
            : base("DefaultConnection")
	    {
            
	    }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configure domain classes using modelBuilder here

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ResumeManager>()
                .HasKey<int>(en => en.Id);

            modelBuilder.Entity<ResumeManager>()
                .HasRequired(prop => prop.Resume)
                .WithRequiredPrincipal(en => en.ResumeManager);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}
