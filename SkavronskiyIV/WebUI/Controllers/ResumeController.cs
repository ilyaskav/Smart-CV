using Repository.Interfaces;
using Services.Classes;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Services.Interfaces;
using System.IO;
using System.Net;

namespace WebUI.Controllers
{
    [Authorize]
    public class ResumeController : Controller
    {
        #region Declarations

        private readonly IResumeService _resumeService = null;
        private readonly IResumeManagerService _managerService = null;
        private readonly IContactService _contactService = null;
        private readonly IInstitutionService _institutionService = null;
        private readonly IProfessionService _professionService = null;
        private readonly IWorkPlaceService _workPlaceService = null;
        private readonly ICertificateService _certificateService = null;
        private readonly ISkillService _skillService = null;

        #endregion

        public ResumeController(IResumeService resumeService, IResumeManagerService managerService, IContactService contactService, 
                                IInstitutionService instService, IProfessionService profService, IWorkPlaceService workService,
                                ICertificateService certService, ISkillService skillService)
        {
            _resumeService = resumeService;
            _managerService = managerService;
            _contactService = contactService;
            _institutionService = instService;
            _professionService = profService;
            _workPlaceService = workService;
            _certificateService = certService;
            _skillService = skillService;
        }

        // GET: Resume
        public ActionResult Index()
        {
            return RedirectToAction("PersonalData");
        }

        [HttpGet]
        [Route("{identifier}")]
        public ActionResult GetWordDoc(Guid identifier)
        {
            if (identifier == null || identifier.Equals(Guid.Empty))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "BadRequest");
            }

            // проверяем, владелец ли резюме шлет запрос 
            if (!_managerService.IsOwnedBy(User.Identity.GetUserId<int>(), identifier))
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            _resumeService.CreateMSWordDocument(identifier);
var manager=_managerService.Get(identifier);
            string projPath = Server.MapPath("~/Content/");
            byte[] fileBytes = System.IO.File.ReadAllBytes(Path.Combine(projPath, "doc", manager.Link));
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, manager.Link);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEmptyResume(ResumeManagerModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.UserId = User.Identity.GetUserId<int>();
            int managerId=_managerService.CreateEmptyResume(model);

            return RedirectToAction(string.Format("PersonalData/{0}", managerId));
        }

        [HttpGet]
        public ActionResult PersonalData(int managerId)
        {
            if (managerId <= 0)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            int userId = User.Identity.GetUserId<int>();

            // проверяем, владелец ли резюме шлет запрос 
            if (!_managerService.IsOwnedBy(userId, managerId))
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            ViewBag.ManagerId = managerId;
            var viewModel = _resumeService.GetResumeByManagerId(managerId);
            if (viewModel == null) return View();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PersonalData(int managerId, ResumeModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            int userId = User.Identity.GetUserId<int>();
            model.ManagerId = managerId;
            // проверяем, владелец ли резюме шлет запрос на его изменение
            if (!_managerService.IsOwnedBy(userId, managerId))
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            if (model.Id == null)
            {
                _resumeService.CreateResume(model);
                
            }
            else
            {
                _resumeService.UpdateResume(model);
            }

            ViewBag.ManagerId = managerId;
            ViewBag.Success = "Изменения сохранены";
            return View(model);
        }

        [HttpGet]
        public ActionResult Contacts(int managerId)
        {
            if (managerId <= 0) return HttpNotFound();

            // проверяем, владелец ли резюме шлет запрос 
            if (!_managerService.IsOwnedBy(User.Identity.GetUserId<int>(), managerId))
            {
                return new HttpUnauthorizedResult();
            }

            ViewBag.ManagerId = managerId;
            var viewModel = _contactService.Get(managerId);
            if (viewModel == null) return View();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contacts(int managerId, ContactAddModel addModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addModel);
            }
            int userId = User.Identity.GetUserId<int>();
            // проверяем, владелец ли резюме шлет запрос на его изменение
            if (!_managerService.IsOwnedBy(userId, managerId))
            {
                return new HttpUnauthorizedResult();
            }

            addModel.ResumeManagerId = managerId;
            _contactService.UpdateContact(addModel);

            ViewBag.Success = "Изменения сохранены";
            return RedirectToAction(string.Format("Contacts/{0}", managerId));
        }

        [HttpGet]
        public ActionResult DeleteContact(int managerId, int contactId)
        {
            // проверяем, владелец ли резюме шлет запрос на его изменение
            if (!_managerService.IsOwnedBy(User.Identity.GetUserId<int>(), managerId))
            {
                return new HttpUnauthorizedResult();
            }

            _contactService.RemoveContact(contactId);

            return RedirectToAction(string.Format("Contacts/{0}", managerId));
        }

        [HttpGet]
        public ActionResult Education(int managerId)
        {
            if (managerId <= 0) return HttpNotFound();

            int userId = User.Identity.GetUserId<int>();
            // проверяем, владелец ли резюме шлет запрос на его изменение
            if (!_managerService.IsOwnedBy(userId, managerId))
            {
                return new HttpUnauthorizedResult();
            }

            ViewBag.ManagerId = managerId;
            var viewModel = _institutionService.Get(managerId);
            if (viewModel == null) return View();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Education(int managerId, InstitutionAddModel addModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addModel);
            }
            int userId = User.Identity.GetUserId<int>();
            // проверяем, владелец ли резюме шлет запрос на его изменение
            if (!_managerService.IsOwnedBy(userId, managerId))
            {
                return new HttpUnauthorizedResult();
            }

            //addModel.ResumeManagerId = managerId;
            _institutionService.CreateOrUpdate (addModel);

            return RedirectToAction(string.Format("Education/{0}", managerId));
        }

        [HttpGet]
        public ActionResult RemoveInstitution(int managerId, int institutionId)
        {
            // проверяем, владелец ли резюме шлет запрос на его изменение
            if (!_managerService.IsOwnedBy(User.Identity.GetUserId<int>(), managerId))
            {
                return new HttpUnauthorizedResult();
            }

            _institutionService.RemoveInstitution(institutionId);

            return RedirectToAction(string.Format("Education/{0}", managerId));
        }

        [HttpGet]
        public ActionResult Manage()
        {
            var userId = User.Identity.GetUserId<int>();

            ViewBag.ProfessionId = new SelectList(_professionService.GetAll(), "Id", "Name");
            var viewModel = _managerService.GetAllResumes(userId);
            if (viewModel == null) return View();

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Remove(int managerId)
        {
            // проверяем, владелец ли резюме шлет запрос на его изменение
            if (!_managerService.IsOwnedBy(User.Identity.GetUserId<int>(), managerId))
            {
                return new HttpUnauthorizedResult();
            }

            _managerService.DeleteResume(managerId);

            return RedirectToAction("Manage");
        }

        [HttpGet]
        public ActionResult Copy(int managerId)
        {
            // проверяем, владелец ли резюме шлет запрос на его изменение
            if (!_managerService.IsOwnedBy(User.Identity.GetUserId<int>(), managerId))
            {
                return new HttpUnauthorizedResult();
            }

            _managerService.CopyResume(managerId);
            return RedirectToAction("Manage");
        }

        [HttpGet]
        public ActionResult WorkExperience(int managerId)
        {
            if (managerId <= 0) return HttpNotFound();

            int userId = User.Identity.GetUserId<int>();
            // проверяем, владелец ли резюме шлет запрос на его изменение
            if (!_managerService.IsOwnedBy(userId, managerId))
            {
                return new HttpUnauthorizedResult();
            }

            ViewBag.ManagerId = managerId;
            var viewModel = _workPlaceService.Get(managerId);
            if (viewModel == null) return View();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WorkExperience(int managerId, WorkPlaceAddModel addModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addModel);
            }
            int userId = User.Identity.GetUserId<int>();
            // проверяем, владелец ли резюме шлет запрос на его изменение
            if (!_managerService.IsOwnedBy(userId, managerId))
            {
                return new HttpUnauthorizedResult();
            }

            addModel.ResumeManagerId = managerId;
            _workPlaceService.CreateOrUpdate(addModel);

            return RedirectToAction(string.Format("WorkExperience/{0}", managerId));
        }

        [HttpGet]
        public ActionResult RemoveWork(int managerId, int workPlaceId)
        {
            // проверяем, владелец ли резюме шлет запрос на его изменение
            if (!_managerService.IsOwnedBy(User.Identity.GetUserId<int>(), managerId))
            {
                return new HttpUnauthorizedResult();
            }

            _workPlaceService.RemoveWorkplace(workPlaceId);

            return RedirectToAction(string.Format("WorkExperience/{0}", managerId));
        }

        [HttpGet]
        public ActionResult RemoveDuty(int managerId, int dutyId)
        {
            // проверяем, владелец ли резюме шлет запрос на его изменение
            if (!_managerService.IsOwnedBy(User.Identity.GetUserId<int>(), managerId))
            {
                return new HttpUnauthorizedResult();
            }

            _workPlaceService.RemoveDuty(dutyId);

            return RedirectToAction(string.Format("WorkExperience/{0}", managerId));
        }

        [HttpGet]
        public ActionResult Skills(int managerId)
        {
            if (managerId <= 0) return HttpNotFound();

            int userId = User.Identity.GetUserId<int>();
            // проверяем, владелец ли резюме шлет запрос на его изменение
            if (!_managerService.IsOwnedBy(userId, managerId))
            {
                return new HttpUnauthorizedResult();
            }

            ViewBag.ManagerId = managerId;
            var viewModel = _skillService.Get(managerId);
            if (viewModel == null) return View();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Skills(int managerId, SkillLanguageAddModel addModel)
        {
           if (!ModelState.IsValid)
            {
                return View(addModel);
            }
            int userId = User.Identity.GetUserId<int>();
            // проверяем, владелец ли резюме шлет запрос на его изменение
            if (!_managerService.IsOwnedBy(userId, managerId))
            {
                return new HttpUnauthorizedResult();
            }

            addModel.ResumeManagerId = managerId;
            _skillService.CreateOrUpdate(addModel);

            return RedirectToAction(string.Format("Skills/{0}", managerId));
        }

        [HttpGet]
        public ActionResult RemoveSkill(int managerId, int skillId)
        {
            // проверяем, владелец ли резюме шлет запрос на его изменение
            if (!_managerService.IsOwnedBy(User.Identity.GetUserId<int>(), managerId))
            {
                return new HttpUnauthorizedResult();
            }

            _skillService.RemoveSkill(skillId);

            return RedirectToAction(string.Format("Skills/{0}", managerId));
        }

        public ActionResult PersonalQualities()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Certificates(int managerId)
        {
            if (managerId <= 0) return HttpNotFound();

            int userId = User.Identity.GetUserId<int>();
            // проверяем, владелец ли резюме шлет запрос на его изменение
            if (!_managerService.IsOwnedBy(userId, managerId))
            {
                return new HttpUnauthorizedResult();
            }

            ViewBag.ManagerId = managerId;
            var viewModel = _certificateService.Get(managerId);
            if (viewModel == null) return View();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Certificates(int managerId, CertificateAddModel addModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addModel);
            }
            int userId = User.Identity.GetUserId<int>();
            // проверяем, владелец ли резюме шлет запрос на его изменение
            if (!_managerService.IsOwnedBy(userId, managerId))
            {
                return new HttpUnauthorizedResult();
            }

            addModel.ResumeManagerId = managerId;
            _certificateService.CreateOrUpdate(addModel);

            return RedirectToAction(string.Format("Certificates/{0}", managerId));
        }

        [HttpGet]
        public ActionResult RemoveCertificate(int managerId, int certificateId)
        {
            // проверяем, владелец ли резюме шлет запрос на его изменение
            if (!_managerService.IsOwnedBy(User.Identity.GetUserId<int>(), managerId))
            {
                return new HttpUnauthorizedResult();
            }

            _certificateService.RemoveCertificate(certificateId);

            return RedirectToAction(string.Format("Certificates/{0}", managerId));
        }
    }
}