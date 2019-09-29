using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartCV.Service.Interfaces;
using SmartCV.WebUI.Models;

namespace SmartCV.WebUI.Controllers
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

        public IActionResult Index()
        {
            ViewBag.ProfessionId = new SelectList(_professionService.GetAll(), "Id", "Name");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
