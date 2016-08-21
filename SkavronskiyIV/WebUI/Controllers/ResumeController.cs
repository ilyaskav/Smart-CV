using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    //[Authorize]
    public class ResumeController : Controller
    {
        // GET: Resume
        public ActionResult Index()
        {
            return View();
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