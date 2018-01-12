using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.Containers;
using System;
using System.Web;
using System.Web.Mvc;
using WeChat.Application;
using WeChat.Domain.AggregatesModel;
using Zap.WeChat.SDK;
using Zap.WeChat.SDK.Cache;

namespace WeChat.Dev.Controllers
{
    /// <summary>
    /// 微信相关操作
    /// </summary>
    public class WeChatController : BaseController
    {
        const string corpId = "wx2e8cc6975a5fa1ce";

        IWeChatAppService _currentService;
        IAccountService _accountService;

        public WeChatController(IWeChatAppService currentService
            , IAccountService accountService)
        {
            _currentService = currentService;
            _accountService = accountService;
        }

        // GET: WeChat
        public ActionResult Index()
        {
            var app = _currentService.GetApp(Constants.MOBILE_APPROVAL_TEST);
            return View(app);
        }

        /// <summary>
        /// 微信网页授权
        /// </summary>
        /// <param name="appcode">应用编码 默认为移动审批</param>
        /// <param name="returnUrl">返回url</param>
        /// <returns></returns>
        public ActionResult Authorize(string appcode = null, string returnUrl = null)
        {
            //默认移动审批应用
            appcode = appcode ?? Constants.MOBILE_APPROVAL_TEST;
            returnUrl = returnUrl ?? Request.UrlReferrer?.ToString();
            var redirectUrl = $"http://douhua.oicp.net/wechat/consumecode?appcode={appcode}&returnUrl={returnUrl}";
            var target = OAuth2Api.GetCode(corpId, redirectUrl, "", "");
            return Redirect(target);
        }

        /// <summary>
        /// 消费临时code 换取微信应用信息
        /// </summary>
        /// <param name="code">临时授权码code</param>
        /// <param name="appcode">应用code</param>
        /// <param name="returnUrl">返回url</param>
        /// <returns></returns>
        public ActionResult ConsumeCode(string code, string appcode, string returnUrl = null)
        {
            try
            {
                User user = null;
                if (string.IsNullOrEmpty(appcode))
                    throw new ArgumentNullException(nameof(appcode));
                var app = _currentService.GetApp(appcode);
                var accessToken = AccessTokenContainer.GetToken(corpId, app.SecretValue);
                var result = OAuth2Api.GetUserId(accessToken, code);
                if (!string.IsNullOrEmpty(result.OpenId))
                {
                    //成员未关注该企业微信
                    throw new Exception("成员未关注该企业微信");
                }
                else if (!string.IsNullOrEmpty(result.UserId))
                {
                    //通过企业微信usesrid 换 业务系统账号密码
                    user = _accountService.FindUserByWxUserID(result.UserId);
                    if (user == null)
                    {
                        //微信用户未绑定对应业务系统账号
                        return RedirectToAction("login", "account", new { returnUrl, appcode, workUserId = result.UserId });
                    }
                    else
                    {
                        var nonceStr = Guid.NewGuid().ToString().Replace("-", "");
                        LocalCacheManager.Add(nonceStr, user);
                        //微信网页授权成功 准备获取企业应用授权
                        return RedirectToAction("worklogin", "account", new { nonce = nonceStr, appcode, returnUrl });
                    }
                }
                else
                {
                    throw new Exception(result.errmsg);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("error", "home", new { errmsg = ex.Message, appcode });
            }
        }
    }
}