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
            ViewBag.Message = "Your application description page.1111222";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.11112222";

            return View();
        }
    }
}