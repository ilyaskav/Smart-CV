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

        #endregion

        public ResumeController(IResumeService resumeService)
        {
            _resumeService = resumeService;
        }

        // GET: Resume
        public ActionResult Index()
        {
            return RedirectToAction("PersonalData");
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public ActionResult PersonalData(int? id)
        {
            if (id == null)
            {
                return View();
            }

            var viewModel =_resumeService.GetResume(id.Value);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PersonalData(ResumeModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.UserId = User.Identity.GetUserId<int>();

            if (model.Id == null)
            {
                _resumeService.CreateResume(model);
                //return Redirect("~/successful-create");
            }
            else
            {
                _resumeService.UpdateResume(model);
                //return Redirect("~/successful-update");
            }

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