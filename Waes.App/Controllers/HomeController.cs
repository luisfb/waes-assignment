using System.Web.Mvc;

namespace Waes.App.Controllers
{
    public class HomeController : Controller
    {

        [OutputCache(Duration = 1800)]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
