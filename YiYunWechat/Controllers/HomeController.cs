using System.Web.Mvc;

namespace YiYunWechat.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.1111";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.1111";

            return View();
        }
    }
}