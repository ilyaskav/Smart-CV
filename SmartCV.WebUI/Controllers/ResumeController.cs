using System;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartCV.Service.Interfaces;
using SmartCV.Service.Models;

namespace SmartCV.WebUI.Controllers
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
        private readonly IHostingEnvironment _hostingEnvironment = null;

        #endregion

        public ResumeController(IResumeService resumeService, IResumeManagerService managerService, IContactService contactService,
                                IInstitutionService instService, IProfessionService profService, IWorkPlaceService workService,
                                ICertificateService certService, ISkillService skillService, IHostingEnvironment hostingEnvironment)
        {
            _resumeService = resumeService;
            _managerService = managerService;
            _contactService = contactService;
            _institutionService = instService;
            _professionService = profService;
            _workPlaceService = workService;
            _certificateService = certService;
            _skillService = skillService;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Resume
        public IActionResult Index()
        {
            return RedirectToAction("PersonalData");
        }

        [HttpGet]
        [Route("{identifier}")]
        public IActionResult GetWordDoc(Guid identifier)
        {
            if (identifier == null || identifier.Equals(Guid.Empty))
            {
                return BadRequest();
            }

            // проверяем, владелец ли резюме шлет запрос 
            if (!_managerService.IsOwnedBy(long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)), identifier))
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            _resumeService.CreateMSWordDocument(identifier);
            var manager = _managerService.Get(identifier);
            byte[] fileBytes = System.IO.File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, "doc", manager.Link));
            return File(fileBytes, MediaTypeNames.Application.Octet, manager.Link);
        }

        [HttpGet]
        [Route("{identifier}")]
        public IActionResult GetPDF(Guid identifier)
        {
            if (identifier == null || identifier.Equals(Guid.Empty))
            {
                return BadRequest();
            }

            // проверяем, владелец ли резюме шлет запрос 
            if (!_managerService.IsOwnedBy(long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)), identifier))
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            _resumeService.CreatePDF(identifier);
            var manager = _managerService.Get(identifier);

            var fileName = manager.Link.Substring(0, manager.Link.Length - 4);
            byte[] fileBytes = System.IO.File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, "doc", fileName + ".pdf"));
            return File(fileBytes, MediaTypeNames.Application.Octet, fileName + ".pdf");
        }

        [HttpGet]
        public IActionResult GetRules(int managerId)
        {
            var json = _professionService.GetRule(managerId);
            return Content(json);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEmptyResume(ResumeModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.UserId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            int managerId = _managerService.CreateEmptyResume(model);

            return RedirectToAction("PersonalData", new { managerId });
        }

        [HttpGet]
        public IActionResult PersonalData(int managerId) //NOT PASSED
        {
            if (managerId <= 0)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            long userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // проверяем, владелец ли резюме шлет запрос 
            if (!_managerService.IsOwnedBy(userId, managerId))
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            ViewBag.ManagerId = managerId;
            var viewModel = _resumeService.GetPersonalDataByResumeId(managerId);
            if (viewModel == null) return View();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PersonalData(int managerId, PersonalDataModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            long userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            model.ResumeId = managerId;
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
        public IActionResult Contacts(int managerId)
        {
            if (managerId <= 0) return NotFound();

            // проверяем, владелец ли резюме шлет запрос 
            if (!_managerService.IsOwnedBy(long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)), managerId))
            {
                return Forbid();
            }

            ViewBag.ManagerId = managerId;
            var viewModel = _contactService.Get(managerId);
            if (viewModel == null) return View();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contacts(int managerId, ContactAddModel addModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addModel);
            }
            long userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            // проверяем, владелец ли резюме шлет запрос на его изменение
            if (!_managerService.IsOwnedBy(userId, managerId))
            {
                return Forbid();
            }

            addModel.ResumeManagerId = managerId;
            _contactService.UpdateContact(addModel);

            ViewBag.Success = "Изменения сохранены";
            return RedirectToAction(string.Format("Contacts/{0}", managerId));
        }

        [HttpGet]
        public IActionResult DeleteContact(int managerId, int contactId)
        {
            // проверяем, владелец ли резюме шлет запрос на его изменение
            if (!_managerService.IsOwnedBy(long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)), managerId))
            {
                return Forbid();
            }

            _contactService.RemoveContact(contactId);

            return RedirectToAction(string.Format("Contacts/{0}", managerId));
        }

        [HttpGet]
        public IActionResult Education(int managerId)
        {
            if (managerId <= 0) return NotFound();

            long userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            // проверяем, владелец ли резюме шлет запрос на его изменение
            if (!_managerService.IsOwnedBy(userId, managerId))
            {
                return Forbid();
            }

            ViewBag.ManagerId = managerId;
            var viewModel = _institutionService.Get(managerId);
            if (viewModel == null) return View();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Education(int managerId, InstitutionAddModel addModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addModel);
            }
            long userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            // проверяем, владелец ли резюме шлет запрос на его изменение
            if (!_managerService.IsOwnedBy(userId, managerId))
            {
                return Forbid();
            }

            //addModel.ResumeManagerId = managerId;
            _institutionService.CreateOrUpdate(addModel);

            return RedirectToAction(string.Format("Education/{0}", managerId));
        }

        [HttpGet]
        public IActionResult RemoveInstitution(int managerId, int institutionId)
        {
            // проверяем, владелец ли резюме шлет запрос на его изменение
            if (!_managerService.IsOwnedBy(long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)), managerId))
            {
                return Forbid();
            }

            _institutionService.RemoveInstitution(institutionId);

            return RedirectToAction(string.Format("Education/{0}", managerId));
        }

        [HttpGet]
        public IActionResult Manage()
        {
            var userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            ViewBag.ProfessionId = new SelectList(_professionService.GetAll(), "Id", "Name");
            var viewModel = _managerService.GetAllResumes(userId);
            if (viewModel == null) return View();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Remove(int managerId)
        {
            // проверяем, владелец ли резюме шлет запрос на его изменение
            if (!_managerService.IsOwnedBy(long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)), managerId))
            {
                return Forbid();
            }

            _managerService.DeleteResume(managerId);

            return RedirectToAction("Manage");
        }

        [HttpGet]
        public IActionResult Copy(int managerId)
        {
            // проверяем, владелец ли резюме шлет запрос на его изменение
            if (!_managerService.IsOwnedBy(long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)), managerId))
            {
                return Forbid();
            }

            _managerService.CopyResume(managerId);
            return RedirectToAction("Manage");
        }

        [HttpGet]
        public IActionResult WorkExperience(int managerId)
        {
            if (managerId <= 0) return NotFound();

            long userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            // проверяем, владелец ли резюме шлет запрос на его изменение
            if (!_managerService.IsOwnedBy(userId, managerId))
            {
                return Forbid();
            }

            ViewBag.ManagerId = managerId;
            var viewModel = _workPlaceService.Get(managerId);
            if (viewModel == null) return View();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult WorkExperience(int managerId, WorkPlaceAddModel addModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addModel);
            }
            long userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            // проверяем, владелец ли резюме шлет запрос на его изменение
            if (!_managerService.IsOwnedBy(userId, managerId))
            {
                return Forbid();
            }

            addModel.ResumeManagerId = managerId;
            _workPlaceService.CreateOrUpdate(addModel);

            return RedirectToAction(string.Format("WorkExperience/{0}", managerId));
        }

        [HttpGet]
        public IActionResult RemoveWork(int managerId, int workPlaceId)
        {
            // проверяем, владелец ли резюме шлет запрос на его изменение
            if (!_managerService.IsOwnedBy(long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)), managerId))
            {
                return Forbid();
            }

            _workPlaceService.RemoveWorkplace(workPlaceId);

            return RedirectToAction(string.Format("WorkExperience/{0}", managerId));
        }

        [HttpGet]
        public IActionResult RemoveDuty(int managerId, int dutyId)
        {
            // проверяем, владелец ли резюме шлет запрос на его изменение
            if (!_managerService.IsOwnedBy(long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)), managerId))
            {
                return Forbid();
            }

            _workPlaceService.RemoveDuty(dutyId);

            return RedirectToAction(string.Format("WorkExperience/{0}", managerId));
        }

        [HttpGet]
        public IActionResult Skills(int managerId)
        {
            if (managerId <= 0) return NotFound();

            long userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            // проверяем, владелец ли резюме шлет запрос на его изменение
            if (!_managerService.IsOwnedBy(userId, managerId))
            {
                return Forbid();
            }

            ViewBag.ManagerId = managerId;
            var viewModel = _skillService.Get(managerId);
            if (viewModel == null) return View();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Skills(int managerId, SkillLanguageAddModel addModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addModel);
            }
            long userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            // проверяем, владелец ли резюме шлет запрос на его изменение
            if (!_managerService.IsOwnedBy(userId, managerId))
            {
                return Forbid();
            }

            addModel.ResumeManagerId = managerId;
            _skillService.CreateOrUpdate(addModel);

            return RedirectToAction(string.Format("Skills/{0}", managerId));
        }

        [HttpGet]
        public IActionResult RemoveSkill(int managerId, int skillId)
        {
            // проверяем, владелец ли резюме шлет запрос на его изменение
            if (!_managerService.IsOwnedBy(long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)), managerId))
            {
                return Forbid();
            }

            _skillService.RemoveSkill(skillId);

            return RedirectToAction(string.Format("Skills/{0}", managerId));
        }

        public IActionResult PersonalQualities()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Certificates(int managerId)
        {
            if (managerId <= 0) return NotFound();

            long userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            // проверяем, владелец ли резюме шлет запрос на его изменение
            if (!_managerService.IsOwnedBy(userId, managerId))
            {
                return Forbid();
            }

            ViewBag.ManagerId = managerId;
            var viewModel = _certificateService.Get(managerId);
            if (viewModel == null) return View();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Certificates(int managerId, CertificateAddModel addModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addModel);
            }
            long userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            // проверяем, владелец ли резюме шлет запрос на его изменение
            if (!_managerService.IsOwnedBy(userId, managerId))
            {
                return Forbid();
            }

            addModel.ResumeManagerId = managerId;
            _certificateService.CreateOrUpdate(addModel);

            return RedirectToAction(string.Format("Certificates/{0}", managerId));
        }

        [HttpGet]
        public IActionResult RemoveCertificate(int managerId, int certificateId)
        {
            // проверяем, владелец ли резюме шлет запрос на его изменение
            if (!_managerService.IsOwnedBy(long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)), managerId))
            {
                return Forbid();
            }

            _certificateService.RemoveCertificate(certificateId);

            return RedirectToAction(string.Format("Certificates/{0}", managerId));
        }
    }
}