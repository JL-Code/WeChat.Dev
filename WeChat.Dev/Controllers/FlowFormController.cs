using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeChat.Dev.Controllers
{
    /// <summary>
    /// 流程表单
    /// </summary>
    public class FlowFormController : Controller
    {
        /// <summary>
        /// 流程表单详情
        /// </summary>
        /// <returns></returns>
        public ActionResult FormDetail()
        {
            return View();
        }
    }
}