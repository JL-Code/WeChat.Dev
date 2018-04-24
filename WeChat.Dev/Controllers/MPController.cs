using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeChat.Application;

namespace WeChat.Dev.Controllers
{
    public class MPController : Controller
    {
        const string corpId = "wx2e8cc6975a5fa1ce";

        IWeChatAppService _currentService;
        IAccountService _accountService;

        public MPController(IWeChatAppService currentService, IAccountService accountService)
        {
            _currentService = currentService;
            _accountService = accountService;
        }

        /// <summary>
        /// 公众平台网页授权
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}