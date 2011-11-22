using System.Web.Mvc;
using UserInterface.Constants;

namespace UserInterface.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData[Keys.HomeMessage] = "Complexity through Simplicity";
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
