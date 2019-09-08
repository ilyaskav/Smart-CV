using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartCV.Entity.Classes;

namespace SmartCV.Entity
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<long>, long>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Profession>().HasData(
                new Profession { Id = 1, Name = "IT", Rules = "1" },
                new Profession { Id = 2, Name = "Спорт", Rules = "2" },
                new Profession { Id = 3, Name = "Менеджмент", Rules = "3" }
            );

            builder.Entity<ContactTitle>().HasData(
                new ContactTitle { Id = 1, Title = "EMail" },
                new ContactTitle { Id = 2, Title = "Phone" },
                new ContactTitle { Id = 3, Title = "Сайт" },
                new ContactTitle { Id = 4, Title = "LinkedIn" }
            );
        }

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


    }
}
