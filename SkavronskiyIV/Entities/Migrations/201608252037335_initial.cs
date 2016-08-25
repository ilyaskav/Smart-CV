namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
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
                        CurrentLocation = c.String(),
                        Photo = c.String(),
                        Goal = c.String(nullable: false),
                        ResumeManagerId = c.Int(nullable: false),
                        Profession_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Professions", t => t.Profession_Id)
                .Index(t => t.Profession_Id);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactTitleId = c.Int(nullable: false),
                        Data = c.String(nullable: false),
                        ResumeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContactTitles", t => t.ContactTitleId, cascadeDelete: true)
                .ForeignKey("dbo.Resumes", t => t.ResumeId, cascadeDelete: true)
                .Index(t => t.ContactTitleId)
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
                "dbo.ResumeManagers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        ProfessionId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Professions", t => t.ProfessionId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Resumes", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.UserId)
                .Index(t => t.ProfessionId);
            
            CreateTable(
                "dbo.Professions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Rules = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
                        WorkPlaceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WorkPlaces", t => t.WorkPlaceId, cascadeDelete: true)
                .Index(t => t.WorkPlaceId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
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
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.WorkPlaces", "ResumeId", "dbo.Resumes");
            DropForeignKey("dbo.Projects", "WorkPlaceId", "dbo.WorkPlaces");
            DropForeignKey("dbo.Duties", "WorkPlaceId", "dbo.WorkPlaces");
            DropForeignKey("dbo.Skills", "ResumeId", "dbo.Resumes");
            DropForeignKey("dbo.ResumeManagers", "Id", "dbo.Resumes");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ResumeManagers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ResumeManagers", "ProfessionId", "dbo.Professions");
            DropForeignKey("dbo.Resumes", "Profession_Id", "dbo.Professions");
            DropForeignKey("dbo.PersonalQualities", "ResumeId", "dbo.Resumes");
            DropForeignKey("dbo.LanguageResumes", "Resume_Id", "dbo.Resumes");
            DropForeignKey("dbo.LanguageResumes", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.Institutions", "ResumeId", "dbo.Resumes");
            DropForeignKey("dbo.Contacts", "ResumeId", "dbo.Resumes");
            DropForeignKey("dbo.Contacts", "ContactTitleId", "dbo.ContactTitles");
            DropForeignKey("dbo.Certificates", "ResumeId", "dbo.Resumes");
            DropIndex("dbo.LanguageResumes", new[] { "Resume_Id" });
            DropIndex("dbo.LanguageResumes", new[] { "Language_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Projects", new[] { "WorkPlaceId" });
            DropIndex("dbo.Duties", new[] { "WorkPlaceId" });
            DropIndex("dbo.WorkPlaces", new[] { "ResumeId" });
            DropIndex("dbo.Skills", new[] { "ResumeId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.ResumeManagers", new[] { "ProfessionId" });
            DropIndex("dbo.ResumeManagers", new[] { "UserId" });
            DropIndex("dbo.ResumeManagers", new[] { "Id" });
            DropIndex("dbo.PersonalQualities", new[] { "ResumeId" });
            DropIndex("dbo.Institutions", new[] { "ResumeId" });
            DropIndex("dbo.Contacts", new[] { "ResumeId" });
            DropIndex("dbo.Contacts", new[] { "ContactTitleId" });
            DropIndex("dbo.Resumes", new[] { "Profession_Id" });
            DropIndex("dbo.Certificates", new[] { "ResumeId" });
            DropTable("dbo.LanguageResumes");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Projects");
            DropTable("dbo.Duties");
            DropTable("dbo.WorkPlaces");
            DropTable("dbo.Skills");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Professions");
            DropTable("dbo.ResumeManagers");
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
