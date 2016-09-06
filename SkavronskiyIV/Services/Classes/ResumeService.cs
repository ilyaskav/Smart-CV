using Entities.Classes;
using Repository.Interfaces;
using Services.Converters;
using Services.Interfaces;
using Services.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Word = Microsoft.Office.Interop.Word;

namespace Services.Classes
{
    public class ResumeService : IResumeService
    {
        #region Declarations

        private readonly IResumeRepository _resumeRepository = null;
        private readonly ILanguageRepository _langRepository = null;
        private readonly IResumeManagerRepository _resumeManagerRepository = null;

        #endregion

        public ResumeService(IResumeRepository resumeRepository, ILanguageRepository langRepository, IResumeManagerRepository manRepo)
        {
            _resumeRepository = resumeRepository;
            _langRepository = langRepository;
            _resumeManagerRepository = manRepo;
        }

        public ResumeModel GetResume(int id)
        {
            return _resumeRepository.Get(id).ToModel();
        }

        public ResumeModel GetResumeByManagerId(int managerId)
        {
            var resumeManager = _resumeManagerRepository.Get(managerId);
            if (resumeManager.Resume == null) return null;

            return resumeManager.Resume.ToModel();
        }

        public void CreateResume(ResumeModel model)
        {
            var resumeManager = _resumeManagerRepository.Get(model.ManagerId);
            resumeManager.Resume = model.ToEntity();

            _resumeManagerRepository.Update(resumeManager);
        }

        public void UpdateResume(ResumeModel model)
        {
            if (model.Id == null) return;
            if (_resumeRepository.Has(model.Id.Value))
            {
                var entity = model.ToEntity();
                _resumeRepository.Update(entity);
            }
        }

        public void DeleteResume(int id)
        {
            if (_resumeRepository.Has(id)) _resumeRepository.Remove(id);
        }


        public void Dispose()
        {
            _resumeRepository.Dispose();
            _langRepository.Dispose();
        }


        public void CreateMSWordDocument(Guid identifier)
        {
            var wordApp = new Word.Application();
            wordApp.Visible = false;

            
            string projPath = HttpContext.Current.Server.MapPath("~/Content/");
            wordApp.Documents.Open(projPath + "MSWordTemplates\\template1.dotx");

            var myResume = _resumeManagerRepository.Get(m => m.Guid.Equals(identifier)).First().Resume;
            var doc = wordApp.ActiveDocument;

            doc.Bookmarks["FULLNAME"].Range.Text = myResume.FirstName + " " + myResume.LastName;
            doc.Bookmarks["GOAL"].Range.Text = myResume.Goal;
            doc.Bookmarks["LOCATION"].Range.Text = myResume.CurrentLocation;
            doc.Bookmarks["EMAIL"].Range.Text = myResume.Contacts.FirstOrDefault(c => c.ContactTitle.Title.Equals("EMail")).Data;
            doc.Bookmarks["TELNUM"].Range.Text = myResume.Contacts.FirstOrDefault(c => c.ContactTitle.Title.Equals("Phone")).Data;
            foreach (var contact in myResume.Contacts.Where(c => !c.ContactTitle.Title.Equals("EMail") && !c.ContactTitle.Title.Equals("Phone")))
            {
                doc.Bookmarks["OTHER_CONTACTS"].Range.Text = String.Format("{0}: {1}\n", contact.ContactTitle.Title, contact.Data);
            }

            // ОПЫТ РАБОТЫ
            if (myResume.WorkExp.Count > 0)
            {
                var expTable = doc.Tables[1];
                int workNum = 0;
                foreach (var work in myResume.WorkExp)
                {
                    workNum++;
                    if (workNum > 1) expTable.Rows.Add();

                    // период в который работали
                    expTable.Cell(workNum, 1).Range.Text = string.Format("{0} –\n{1}", work.From.Format(), work.To.Format());

                    // описание работы
                    expTable.Cell(workNum, 2).Range.Text = string.Empty;
                    //  название работы и город
                    var range = doc.Range(expTable.Cell(workNum, 2).Range.Start, expTable.Cell(workNum, 2).Range.Start);

                    range.Text = string.Format("{0}, г. {1}\n", work.Name, work.City);
                    range.Font.Bold = 1;
                    //  должность
                    range.SetRange(range.End, range.End);
                    range.Text = string.Format("{0}\n", work.Position);
                    range.Font.Italic = 1; range.Font.Bold = 0;
                    //  обязанности
                    range.SetRange(range.End, range.End);
                    StringBuilder strBuilder = new StringBuilder();
                    foreach (var duty in work.Duties)
                    {
                        strBuilder.AppendFormat("– {0}\n", duty.Name);
                    }
                    range.Text = strBuilder.ToString();
                    range.Font.Italic = 0; range.Font.Bold = 0;
                }
            }
            else doc.Bookmarks["SECTION_EXPERIENCE"].Range.Text = string.Empty;

            // ОБРАЗОВАНИЕ
            if (myResume.Education.Count > 0)
            {
                var eduTable = doc.Tables[2];
                int eduNum = 0;
                foreach (var institution in myResume.Education)
                {
                    eduNum++;
                    if (eduNum > 1) eduTable.Rows.Add();

                    // период учебы
                    eduTable.Cell(eduNum, 1).Range.Text = string.Format("{0} –\n{1}", institution.From.Format(), institution.To.Format());

                    // описание уч. заведениия:
                    eduTable.Cell(eduNum, 2).Range.Text = string.Empty;
                    var range = doc.Range(eduTable.Cell(eduNum, 2).Range.Start, eduTable.Cell(eduNum, 2).Range.Start);
                    // название и город
                    range.Text = string.Format("{0}, {1}\n", institution.Name, institution.City);
                    range.Font.Bold = 1;
                    // кафедра
                    range.SetRange(range.End, range.End);
                    range.Text = string.Format("{0}\n", institution.Department);
                    range.Font.Bold = 0; range.Font.Italic = 1;
                    // специальность
                    range.SetRange(range.End, range.End);
                    range.Text = string.Format("{0}", institution.Specialty);
                    range.Font.Italic = 0; range.Font.Bold = 0;
                }
            }
            else doc.Bookmarks["SECTION_EDUCATION"].Range.Text = string.Empty;

            // СЕРТИФИКАТЫ
            if (myResume.CertificatesAndTrainings.Count > 0)
            {
                StringBuilder strBuilder = new StringBuilder();
                foreach (var certificate in myResume.CertificatesAndTrainings)
                {
                    strBuilder.AppendFormat("{0} – {1}", certificate.Date.Year, certificate.Name);
                    if (certificate.Location != null) strBuilder.AppendFormat(", г. {0}", certificate.Location);
                    strBuilder.Append("\n");
                }
                doc.Bookmarks["CERTIFICATES"].Range.Text = strBuilder.ToString();
            }
            else doc.Bookmarks["SECTION_CERTIFICATES"].Range.Text = string.Empty;

            // ЯЗЫКИ
            if (myResume.Languages.Count > 0)
            {
                var range = doc.Range(doc.Bookmarks["LANGUAGES"].Range.Start, doc.Bookmarks["LANGUAGES"].Range.End);
                range.Text = string.Empty;

                foreach (var language in myResume.Languages)
                {
                    //var paragraph = range.Paragraphs.Add();
                    range.InsertAfter(string.Format("{0} – {1}\n", language.Name, language.Level));
                }
                range.ListFormat.ApplyBulletDefault();
            }
            else doc.Bookmarks["SECTION_LANGUAGES"].Range.Text = string.Empty;

            // ЛИЧНЫЕ КАЧЕСТВА
            if (myResume.PersonalQualities.Count > 0)
            {
                var range = doc.Range(doc.Bookmarks["PERSONAL_QUALITIES"].Range.Start, doc.Bookmarks["PERSONAL_QUALITIES"].Range.End);
                range.Text = string.Empty;

                foreach (var quality in myResume.PersonalQualities)
                {
                    range.InsertAfter(string.Format("{0}\n", quality.Name));
                }
                range.ListFormat.ApplyBulletDefault();
            }
            else doc.Bookmarks["SECTION_PERSONAL_QUALITIES"].Range.Text = string.Empty;

            // НАВЫКИ
            if (myResume.Skills.Count > 0)
            {
                var range = doc.Range(doc.Bookmarks["SKILLS"].Range.Start, doc.Bookmarks["SKILLS"].Range.End);
                range.Text = string.Empty;

                foreach (var skill in myResume.Skills)
                {
                    range.InsertAfter(string.Format("{0}\n", skill.Name));
                }
                range.ListFormat.ApplyBulletDefault();
            }
            else doc.Bookmarks["SECTION_SKILLS"].Range.Text = string.Empty;

            
            myResume.ResumeManager.Link = string.Format("cv-{0}-{1}.doc", myResume.ResumeManager.Id, myResume.ResumeManager.Profession.Name);
            _resumeManagerRepository.Update(myResume.ResumeManager); 

            doc.SaveAs2(FileName: Path.Combine(projPath, "doc", myResume.ResumeManager.Link));
            doc.Close(SaveChanges: Word.WdSaveOptions.wdDoNotSaveChanges);
            wordApp.Quit();
        }

        
    }
}
