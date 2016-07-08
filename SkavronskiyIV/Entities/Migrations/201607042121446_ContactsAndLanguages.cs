namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContactsAndLanguages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Certificates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Location = c.String(),
                        ResumeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Resumes", t => t.ResumeId, cascadeDelete: true)
                .Index(t => t.ResumeId);
            
            CreateTable(
                "dbo.Resumes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        CurrentLocation = c.String(nullable: false),
                        Photo = c.String(),
                        Goal = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactId = c.Int(nullable: false),
                        Data = c.String(nullable: false),
                        ResumeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContactTitles", t => t.ContactId, cascadeDelete: true)
                .ForeignKey("dbo.Resumes", t => t.ResumeId, cascadeDelete: true)
                .Index(t => t.ContactId)
                .Index(t => t.ResumeId);
            
            CreateTable(
                "dbo.ContactTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Institutions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        City = c.String(),
                        Name = c.String(nullable: false),
                        Department = c.String(),
                        Specialty = c.String(nullable: false),
                        Degree = c.String(nullable: false),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(),
                        ResumeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Resumes", t => t.ResumeId, cascadeDelete: true)
                .Index(t => t.ResumeId);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Level = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PersonalQualities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ResumeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Resumes", t => t.ResumeId, cascadeDelete: true)
                .Index(t => t.ResumeId);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ResumeId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Resumes", t => t.ResumeId, cascadeDelete: true)
                .Index(t => t.ResumeId);
            
            CreateTable(
                "dbo.WorkPlaces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        City = c.String(),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Position = c.String(nullable: false),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(),
                        ResumeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Resumes", t => t.ResumeId, cascadeDelete: true)
                .Index(t => t.ResumeId);
            
            CreateTable(
                "dbo.Duties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        WorkPlaceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WorkPlaces", t => t.WorkPlaceId, cascadeDelete: true)
                .Index(t => t.WorkPlaceId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Duration = c.String(),
                        Description = c.String(nullable: false),
                        Role = c.String(nullable: false),
                        TechStack = c.String(nullable: false),
                        About = c.String(),
                        WorkPlace_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WorkPlaces", t => t.WorkPlace_Id)
                .Index(t => t.WorkPlace_Id);
            
            CreateTable(
                "dbo.LanguageResumes",
                c => new
                    {
                        Language_Id = c.Int(nullable: false),
                        Resume_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Language_Id, t.Resume_Id })
                .ForeignKey("dbo.Languages", t => t.Language_Id, cascadeDelete: true)
                .ForeignKey("dbo.Resumes", t => t.Resume_Id, cascadeDelete: true)
                .Index(t => t.Language_Id)
                .Index(t => t.Resume_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkPlaces", "ResumeId", "dbo.Resumes");
            DropForeignKey("dbo.Projects", "WorkPlace_Id", "dbo.WorkPlaces");
            DropForeignKey("dbo.Duties", "WorkPlaceId", "dbo.WorkPlaces");
            DropForeignKey("dbo.Skills", "ResumeId", "dbo.Resumes");
            DropForeignKey("dbo.PersonalQualities", "ResumeId", "dbo.Resumes");
            DropForeignKey("dbo.LanguageResumes", "Resume_Id", "dbo.Resumes");
            DropForeignKey("dbo.LanguageResumes", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.Institutions", "ResumeId", "dbo.Resumes");
            DropForeignKey("dbo.Contacts", "ResumeId", "dbo.Resumes");
            DropForeignKey("dbo.Contacts", "ContactId", "dbo.ContactTitles");
            DropForeignKey("dbo.Certificates", "ResumeId", "dbo.Resumes");
            DropIndex("dbo.LanguageResumes", new[] { "Resume_Id" });
            DropIndex("dbo.LanguageResumes", new[] { "Language_Id" });
            DropIndex("dbo.Projects", new[] { "WorkPlace_Id" });
            DropIndex("dbo.Duties", new[] { "WorkPlaceId" });
            DropIndex("dbo.WorkPlaces", new[] { "ResumeId" });
            DropIndex("dbo.Skills", new[] { "ResumeId" });
            DropIndex("dbo.PersonalQualities", new[] { "ResumeId" });
            DropIndex("dbo.Institutions", new[] { "ResumeId" });
            DropIndex("dbo.Contacts", new[] { "ResumeId" });
            DropIndex("dbo.Contacts", new[] { "ContactId" });
            DropIndex("dbo.Certificates", new[] { "ResumeId" });
            DropTable("dbo.LanguageResumes");
            DropTable("dbo.Projects");
            DropTable("dbo.Duties");
            DropTable("dbo.WorkPlaces");
            DropTable("dbo.Skills");
            DropTable("dbo.PersonalQualities");
            DropTable("dbo.Languages");
            DropTable("dbo.Institutions");
            DropTable("dbo.ContactTitles");
            DropTable("dbo.Contacts");
            DropTable("dbo.Resumes");
            DropTable("dbo.Certificates");
        }
    }
}
