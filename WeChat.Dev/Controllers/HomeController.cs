using System.Web.Mvc;
using Zap.WeChat.SDK;

namespace WeChat.Dev.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.AppCode = Constants.MOBILE_APPROVAL;
            return View();
        }

        public ActionResult TestAuthroize()
        {
            ViewBag.AppCode = Constants.WECHAT;
            return View();
        }

        public ActionResult Error(string errmsg = null, string appcode = null)
        {
            ViewBag.ErrMsg = errmsg;
            ViewBag.AppCode = appcode ?? Constants.MOBILE_APPROVAL;
            return View();
        }
    }
}