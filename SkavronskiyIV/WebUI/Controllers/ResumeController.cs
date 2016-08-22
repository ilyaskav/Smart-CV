using Repository.Interfaces;
using Services.Classes;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace WebUI.Controllers
{
    [Authorize]
    public class ResumeController : Controller
    {
        #region Declarations

        private readonly IResumeRepository _resumeRepository = null;
        private readonly ILanguageRepository _langRepository = null;
        private readonly ResumeService _resumeService = null;

        #endregion

        public ResumeController()
        {
            _resumeService = new ResumeService(_resumeRepository, _langRepository);
        }

        // GET: Resume
        public ActionResult Index()
        {
            return RedirectToAction("PersonalData");
        }

        [HttpGet]
        public ActionResult PersonalData()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PersonalData(ResumeModel model)
        {
            if (!ModelState.IsValid) return View(model);

            model.UserId = User.Identity.GetUserId<int>();
            _resumeService.CreateResume(model);
            return View(model);
        }


        public ActionResult Contacts()
        {
            return View();
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