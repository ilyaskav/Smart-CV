using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Repository.Interfaces;
using Services.Converters;
using Services.Interfaces;
using Services.Models;
using System;
using System.IO;
using System.Linq;
using System.Web;

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
            var myResume = _resumeManagerRepository
                .Get(m => m.Guid.Equals(identifier) && m.Link != null)
                .Select(m => m.Resume)
                .FirstOrDefault();
            if (myResume is null) throw new NullReferenceException("Resume was not found or it is not completed");

            string projPath = HttpContext.Current.Server.MapPath("~/Content/");
            string outFilePath = Path.Combine(projPath, "doc", myResume.ResumeManager.Link);
            byte[] templateBytes = System.IO.File.ReadAllBytes(projPath + "MSWordTemplates\\template4.dotx");

            using (MemoryStream templateStream = new MemoryStream())
            {
                templateStream.Write(templateBytes, 0, (int)templateBytes.Length);

                using (WordprocessingDocument doc = WordprocessingDocument.Open(templateStream, true))
                {
                    doc.ChangeDocumentType(WordprocessingDocumentType.Document);
                    var mainPart = doc.MainDocumentPart;

                    // Get the Document Settings Part
                    DocumentSettingsPart documentSettingPart1 = mainPart.DocumentSettingsPart;

                    // Create a new attachedTemplate and specify a relationship ID
                    AttachedTemplate attachedTemplate1 = new AttachedTemplate() { Id = "relationId1" };

                    // Append the attached template to the DocumentSettingsPart
                    documentSettingPart1.Settings.Append(attachedTemplate1);

                    // Add an ExternalRelationShip of type AttachedTemplate.
                    // Specify the path of template and the relationship ID
                    documentSettingPart1.AddExternalRelationship("http://schemas.openxmlformats.org/officeDocument/2006/relationships/attachedTemplate", new Uri(projPath + "MSWordTemplates\\template4.dotx", UriKind.Absolute), "relationId1");

                    string fullname = string.Format("{0} {1}", myResume.FirstName, myResume.LastName);
                    SetCCText(mainPart, "FullName", fullname);

                    SetCCText(mainPart, "Goal", myResume.Goal);
                    SetCCText(mainPart, "Location", myResume.CurrentLocation);

                    string email = myResume.Contacts.First(c => c.ContactTitle.Title.Equals("EMail")).Data;
                    SetCCText(mainPart, "Email", email);

                    string phone = myResume.Contacts.First(c => c.ContactTitle.Title.Equals("Phone")).Data;
                    SetCCText(mainPart, "Phone", phone);

                    RemoveCCChild(mainPart, "OtherContacts");
                    foreach (var contact in myResume.Contacts.Where(c => !c.ContactTitle.Title.Equals("EMail") && !c.ContactTitle.Title.Equals("Phone")))
                    {
                        AppendCCText(mainPart, "OtherContacts", string.Format("{0}: {1}", contact.ContactTitle.Title, contact.Data));
                    }

                    // ОБРАЗОВАНИЕ
                    if (myResume.Education.Count > 0)
                    {
                        SdtBlock contentControl = mainPart.Document.Body.Descendants<SdtBlock>().Where(r => r.SdtProperties.GetFirstChild<Tag>().Val == "Section_Education").Single();
                        Table theTable = contentControl.Descendants<Table>().Single();
                        TableRow defaultRow = theTable.Elements<TableRow>().Last();

                        foreach (var institution in myResume.Education)
                        {
                            TableRow rowCopy = (TableRow)defaultRow.CloneNode(true);

                            // период учебы
                            var periodRun = rowCopy.Descendants<TableCell>().ElementAt(0).GetFirstChild<Paragraph>().GetFirstChild<Run>();
                            periodRun.GetFirstChild<Text>().Text = string.Format("{0} –", institution.From.Format());
                            periodRun.Append(new Break());
                            periodRun.Append(new Text(institution.To.Format()));

                            // описание уч. заведениия:
                            // название и город
                            TableCell secondColumn = rowCopy.Descendants<TableCell>().ElementAt(1);
                            SetCCText(secondColumn, "InstitutionName", string.Format("{0}, г. {1}", institution.Name, institution.City));

                            // кафедра
                            if (string.IsNullOrEmpty(institution.Department))
                                secondColumn.Descendants<SdtRun>().Where(r => r.SdtProperties.GetFirstChild<Tag>().Val == "InstitutionDepartment").Single().Parent.Remove();
                            else
                                SetCCText(secondColumn, "InstitutionDepartment", institution.Department);

                            // специальность
                            SetCCText(secondColumn, "InstitutionSpeciality", institution.Specialty);
                            secondColumn.Elements<Paragraph>().Last().Append(new Run(new Break()));

                            theTable.AppendChild(rowCopy);
                        }
                        theTable.RemoveChild(defaultRow);
                    }
                    else mainPart.Document.Body.Descendants<SdtBlock>().Where(r => r.SdtProperties.GetFirstChild<Tag>().Val == "Section_Education").Single().Remove();

                    // ОПЫТ РАБОТЫ
                    if (myResume.WorkExp.Count > 0)
                    {
                        SdtBlock contentControl = mainPart.Document.Body.Descendants<SdtBlock>().Where(r => r.SdtProperties.GetFirstChild<Tag>().Val == "Section_Experience").Single();
                        Table theTable = contentControl.Descendants<Table>().Single();
                        TableRow defaultRow = theTable.Elements<TableRow>().Last();

                        foreach (var workPlace in myResume.WorkExp)
                        {
                            TableRow rowCopy = (TableRow)defaultRow.CloneNode(true);

                            // период в который работали
                            var periodRun = rowCopy.Descendants<TableCell>().ElementAt(0).GetFirstChild<Paragraph>().GetFirstChild<Run>();
                            periodRun.GetFirstChild<Text>().Text = string.Format("{0} –", workPlace.From.Format());
                            periodRun.Append(new Break());
                            periodRun.Append(new Text(workPlace.To.Format()));

                            // описание работы:
                            // название работы и город
                            TableCell secondColumn = rowCopy.Descendants<TableCell>().ElementAt(1);
                            SetCCText(secondColumn, "WorkplaceName", string.Format("{0}, г. {1}", workPlace.Name, workPlace.City));

                            // должность
                            SetCCText(secondColumn, "WorkplacePosition", workPlace.Position);

                            // обязанности
                            if (workPlace.Duties.Count > 0)
                            {
                                RemoveCCChild(secondColumn, "WorkplaceDuties");
                                foreach (var duty in workPlace.Duties)
                                {
                                    AppendCCText(secondColumn, "WorkplaceDuties", string.Format("– {0}", duty.Name));
                                }
                            }
                            else
                            {
                                secondColumn.Descendants<SdtRun>().Where(r => r.SdtProperties.GetFirstChild<Tag>().Val == "WorkplaceDuties").Single().Parent.Remove();
                            }

                            //secondColumn.Elements<Paragraph>().Last().Append(new Run(new Break()));

                            theTable.AppendChild(rowCopy);
                        }
                        theTable.RemoveChild(defaultRow);
                    }
                    else mainPart.Document.Body.Descendants<SdtBlock>().Where(r => r.SdtProperties.GetFirstChild<Tag>().Val == "Section_Experience").Single().Remove();


                    // СЕРТИФИКАТЫ
                    if (myResume.CertificatesAndTrainings.Count > 0)
                    {
                        RemoveCCChild(mainPart, "Certificates");
                        foreach (var certificate in myResume.CertificatesAndTrainings)
                        {
                            AppendCCText(mainPart, "Certificates", string.Format("{0} – {1}{2}", certificate.Date.Year, certificate.Name, certificate.Location != null ? string.Format(", г. {0}", certificate.Location) : ""));
                        }
                    }
                    else RemoveCC(mainPart, "Section_Certificates");

                    // ЯЗЫКИ
                    if (myResume.Languages.Count > 0)
                    {
                        SdtBlock contentControl = mainPart.Document.Body.Descendants<SdtBlock>().Where(r => r.SdtProperties.GetFirstChild<Tag>().Val == "Languages").Single();
                        SdtContentBlock contentRun = contentControl.GetFirstChild<SdtContentBlock>();
                        Paragraph defaultLi = contentRun.GetFirstChild<Paragraph>();

                        foreach (var language in myResume.Languages)
                        {
                            Paragraph copy = (Paragraph)defaultLi.CloneNode(true);
                            copy.Descendants<Text>().Where(t => t.Text == "lang").Single().Text = language.Name;
                            copy.Descendants<Text>().Where(t => t.Text == "level").Single().Text = language.Level;
                            contentRun.Append(copy);
                        }
                        contentRun.RemoveChild(defaultLi);
                    }
                    else RemoveCC(mainPart, "Section_Languages");

                    // ЛИЧНЫЕ КАЧЕСТВА
                    if (myResume.PersonalQualities.Count > 0)
                    {
                        SdtBlock contentControl = mainPart.Document.Body.Descendants<SdtBlock>().Where(r => r.SdtProperties.GetFirstChild<Tag>().Val == "PersonalQualities").Single();
                        SdtContentBlock contentRun = contentControl.GetFirstChild<SdtContentBlock>();
                        Paragraph defaultLi = contentRun.GetFirstChild<Paragraph>();

                        foreach (var quality in myResume.PersonalQualities)
                        {
                            Paragraph copy = (Paragraph)defaultLi.CloneNode(true);
                            copy.Descendants<Text>().Where(t => t.Text == "quality").Single().Text = quality.Name;
                            contentRun.Append(copy);
                        }
                        contentRun.RemoveChild(defaultLi);
                    }
                    else
                    {
                        mainPart.Document.Body.Descendants<SdtBlock>().Where(r => r.SdtProperties.GetFirstChild<Tag>().Val == "Section_PersonalQualities").Single().Remove();
                    }

                    // НАВЫКИ
                    if (myResume.Skills.Count > 0)
                    {
                        SdtBlock contentControl = mainPart.Document.Body.Descendants<SdtBlock>().Where(r => r.SdtProperties.GetFirstChild<Tag>().Val == "Skills").Single();
                        SdtContentBlock contentRun = contentControl.GetFirstChild<SdtContentBlock>();
                        Paragraph defaultLi = contentRun.GetFirstChild<Paragraph>();

                        foreach (var skill in myResume.Skills)
                        {
                            Paragraph copy = (Paragraph)defaultLi.CloneNode(true);
                            copy.Descendants<Text>().Where(t => t.Text == "skill").Single().Text = skill.Name;
                            contentRun.Append(copy);
                        }
                        contentRun.RemoveChild(defaultLi);
                    }
                    else mainPart.Document.Body.Descendants<SdtBlock>().Where(r => r.SdtProperties.GetFirstChild<Tag>().Val == "Section_Skills").Single().Remove();

                    mainPart.Document.Save();
                }
                File.WriteAllBytes(outFilePath, templateStream.ToArray());
            }
        }

        private void SetCCText(MainDocumentPart mainPart, string contentControlTag, string text)
        {
            SdtRun contentControl = mainPart.Document.Body.Descendants<SdtRun>().Where(r => r.SdtProperties.GetFirstChild<Tag>().Val == contentControlTag).Single();
            SdtContentRun contentRun = contentControl.GetFirstChild<SdtContentRun>();
            contentRun.GetFirstChild<Run>().GetFirstChild<Text>().Text = text;
        }

        private void SetCCText(TableCell tableCell, string contentControlTag, string text)
        {
            SdtRun contentControl = tableCell.Descendants<SdtRun>().Where(r => r.SdtProperties.GetFirstChild<Tag>().Val == contentControlTag).Single();
            SdtContentRun contentRun = contentControl.GetFirstChild<SdtContentRun>();
            contentRun.GetFirstChild<Run>().GetFirstChild<Text>().Text = text;
        }

        private void RemoveCC(MainDocumentPart mainPart, string contentControlTag)
        {
            SdtRun contentControl = mainPart.Document.Body.Descendants<SdtRun>().Where(r => r.SdtProperties.GetFirstChild<Tag>().Val == contentControlTag).SingleOrDefault();

            var breaks = contentControl.Parent.Descendants<Break>();
            foreach (var item in breaks)
            {
                item.Remove();
            }
            contentControl.Remove();
        }

        private void AppendCCText(MainDocumentPart mainPart, string contentControlTag, string text, bool clearCC = false)
        {
            SdtRun contentControl = mainPart.Document.Body.Descendants<SdtRun>().Where(r => r.SdtProperties.GetFirstChild<Tag>().Val == contentControlTag).Single();
            SdtContentRun contentRun = contentControl.GetFirstChild<SdtContentRun>();

            //if (clearCC == true) contentRun.RemoveAllChildren<Text>();

            Text textElement = contentRun.GetFirstChild<Run>().AppendChild(new Text(text));
            textElement.InsertAfterSelf(new Break());
        }

        private void AppendCCText(TableCell tableCell, string contentControlTag, string text)
        {
            SdtRun contentControl = tableCell.Descendants<SdtRun>().Where(r => r.SdtProperties.GetFirstChild<Tag>().Val == contentControlTag).Single();
            SdtContentRun contentRun = contentControl.GetFirstChild<SdtContentRun>();

            Text textElement = contentRun.GetFirstChild<Run>().AppendChild(new Text(text));
            textElement.InsertAfterSelf(new Break());
        }

        private void RemoveCCChild(MainDocumentPart mainPart, string contentControlTag)
        {
            SdtRun contentControl = mainPart.Document.Body.Descendants<SdtRun>().Where(r => r.SdtProperties.GetFirstChild<Tag>().Val == contentControlTag).Single();
            Run contentRun = contentControl.GetFirstChild<SdtContentRun>().GetFirstChild<Run>();

            contentRun.RemoveAllChildren<Text>();
        }

        private void RemoveCCChild(TableCell tableCell, string contentControlTag)
        {
            SdtRun contentControl = tableCell.Descendants<SdtRun>().Where(r => r.SdtProperties.GetFirstChild<Tag>().Val == contentControlTag).Single();
            Run contentRun = contentControl.GetFirstChild<SdtContentRun>().GetFirstChild<Run>();

            contentRun.RemoveAllChildren<Text>();
        }

        public void CreatePDF(Guid identifier)
        {
            var myResume = _resumeManagerRepository.Get(m => m.Guid.Equals(identifier)).First().Resume;
            string projPath = HttpContext.Current.Server.MapPath("~/Content/");

            if (!File.Exists(projPath + "doc\\" + myResume.ResumeManager.Link)) CreateMSWordDocument(identifier);

            //var wordApp = new Word.Application();
            //wordApp.Visible = false;

            //wordApp.Documents.Open(projPath + "doc\\" + myResume.ResumeManager.Link);

            //var fileName = myResume.ResumeManager.Link.Substring(0, myResume.ResumeManager.Link.Length - 4);
            //var doc = wordApp.ActiveDocument;
            //doc.SaveAs2(Path.Combine(projPath, "doc", fileName), WdSaveFormat.wdFormatPDF);
            //doc.Close();
            //wordApp.Quit();
        }
    }
}
