using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.Containers;
using System;
using System.Web.Mvc;
using Zap.WeChat.SDK;

namespace WeChat.Dev.Controllers
{
    /// <summary>
    /// 微信相关操作
    /// </summary>
    public class WeChatController : BaseController
    {
        const string corpId = "wx2e8cc6975a5fa1ce";

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

        /// <summary>
        /// 微信网页授权
        /// </summary>
        /// <param name="appcode">应用编码 默认为移动审批</param>
        /// <returns></returns>
        public ActionResult Authorize(string appcode = null)
        {
            //默认移动审批应用
            appcode = appcode ?? Constants.MOBILE_APPROVAL;
            var redirectUrl = $"http://meunsc.oicp.net/wechat/consumecode?appcode={appcode}";
            var target = OAuth2Api.GetCode(corpId, redirectUrl, "");
            return Redirect(target);
        }

        public ActionResult ConsumeCode(string code, string appcode)
        {
            try
            {
                if (string.IsNullOrEmpty(appcode))
                    throw new ArgumentNullException(nameof(appcode));
                var app = _currentService.GetApp(appcode);
                var accessToken = AccessTokenContainer.GetToken(corpId, app.SecretValue);
                var result = OAuth2Api.GetUserId(accessToken, code);
                if (!string.IsNullOrEmpty(result.OpenId))
                {
                    //成员未关注该企业微信
                    return Content("成员未关注该企业微信");
                }
                else if (!string.IsNullOrEmpty(result.UserId))
                {
                    //身份合法 跳转首页
                    return Redirect("/home/index");
                }
                else
                {
                    return Content(result.errmsg);
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }
}