using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeChat.Dev.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TestAuthroize()
        {
            return View();
        }

        public ActionResult Error(string errmsg = null)
        {
            ViewBag.ErrMsg = errmsg;
            return View();
        }
    }
}