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

namespace WebUI.Controllers
{
    [Authorize]
    public class ResumeController : Controller
    {
        #region Declarations

        private readonly IResumeService _resumeService = null;
        private readonly IResumeManagerService _managerService = null;
        private readonly IContactService _contactService = null;

        #endregion

        public ResumeController(IResumeService resumeService, IResumeManagerService managerService, IContactService contactService)
        {
            _resumeService = resumeService;
            _managerService = managerService;
            _contactService = contactService;
        }

        // GET: Resume
        public ActionResult Index()
        {
            return RedirectToAction("PersonalData");
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

            //return RedirectToAction("PersonalData", new { id = resumeId });
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

            var viewModel = _contactService.Get(managerId);
            if (viewModel == null) return View();

            return View(viewModel);
        }

        [HttpPost]
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
            return View(addModel);
        }

        public ActionResult Education()
        {
            return View();
        }

        public ActionResult WorkExperience()
        {
            return View();
        }

        public ActionResult Skills()
        {
            return View();
        }

        public ActionResult PersonalQualities()
        {
            return View();
        }

        public ActionResult Certificates()
        {
            return View();
        }
    }
}