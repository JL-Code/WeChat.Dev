using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeChat.Dev.Controllers
{
    public class AppController : Controller
    {
        // GET: App
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 待办
        /// </summary>
        /// <returns></returns>
        public ActionResult ToDos()
        {
            return View();
        }

        /// <summary>
        /// 申请
        /// </summary>
        /// <returns></returns>
        public ActionResult Application()
        {
            return View();
        }

        /// <summary>
        /// 办理
        /// </summary>
        /// <returns></returns>
        public ActionResult Transaction()
        {
            return View();
        }

        /// <summary>
        /// 抄送
        /// </summary>
        /// <returns></returns>
        public ActionResult CCNotation()
        {
            return View();
        }
    }
}