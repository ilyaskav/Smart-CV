using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        #region declarations

        private readonly IProfessionService _professionService = null;
        #endregion

        public HomeController(IProfessionService profService)
        {
            _professionService = profService;
        }

        public ActionResult Index()
        {
             
            ViewBag.ProfessionId = new SelectList(_professionService.GetAll(), "Id", "Name");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}