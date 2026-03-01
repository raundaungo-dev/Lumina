using System.Web.Mvc;

namespace LuminaPortal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() => View();

        // This handles the News and the Transparency Seal
        public ActionResult About()
        {
            ViewBag.Message = "Official Transparency Board";
            return View();
        }

        // NEW: This handles the Quick Links and Hotline
        public ActionResult Services()
        {
            ViewBag.Message = "Citizen Services Directory";
            return View();
        }
    }
}