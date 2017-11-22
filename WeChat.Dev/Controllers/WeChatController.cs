using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeChat.Core;

namespace WeChat.Dev.Controllers
{
    /// <summary>
    /// 微信相关操作
    /// </summary>
    public class WeChatController : BaseController
    {
        IWeChatAppService _currentService;
        public WeChatController(IWeChatAppService currentService)
        {
            _currentService = currentService;
        }
        // GET: WeChat
        public ActionResult Index()
        {
            var app = _currentService.GetApp(Constants.MOBILE_APPROVAL);
            return View(app);
        }
    }
}