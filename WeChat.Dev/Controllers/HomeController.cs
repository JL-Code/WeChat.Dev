using System.Web.Mvc;
using WeChat.Infrastructure;
using Zap.WeChat.SDK;
using Zap.WeChat.SDK.Cache;
using Zap.WeChat.SDK.Handler;
using Zap.WeChat.SDK.IServices;

namespace WeChat.Dev.Controllers
{
    public class HomeController : Controller
    {
        ICorpAppService _corpAppService;
        public HomeController(ICorpAppService corpAppService)
        {
            _corpAppService = corpAppService;
        }
        public ActionResult Index()
        {
            ViewBag.AppCode = Constants.MOBILE_APPROVAL_TEST;
            ViewBag.WebSite = "http://douhua.oicp.net";
            return View();
        }

        public ActionResult TestAuthroize()
        {
            ViewBag.AppCode = Constants.WECHAT;
            ViewBag.WebSite = "http://douhua.oicp.net";
            return View();
        }

        public ActionResult Error(string errmsg = null, string appcode = null)
        {
            ViewBag.WebSite = "http://douhua.oicp.net";
            ViewBag.ErrMsg = errmsg;
            ViewBag.AppCode = appcode ?? Constants.MOBILE_APPROVAL_TEST;
            return View();
        }

        public ActionResult ListPanel()
        {
            return View();
        }

        public ActionResult MyApplication()
        {
            return View();
        }

        public ActionResult MyTransaction()
        {
            return View();
        }

        public ActionResult MyCarbonCopy()
        {
            return View();
        }

        public ActionResult Detail()
        {
            return View();
        }

        public ActionResult BusinessHandlePage(string currentUrl = null)
        {
            currentUrl = currentUrl ?? Request.Url.ToString().Replace(":8070", "");
            //JSSDK 签名
            var handler = new JSSDKHandler(_corpAppService, Constants.MOBILE_APPROVAL_TEST);

            var result = handler.GetSignature(currentUrl);
            var sdkConfigStr = JsonHandler.ToJson(new
            {
                appId = result.AppId, // 必填，企业号的唯一标识，此处填写企业号corpid
                timestamp = result.Timestamp, // 必填，生成签名的时间戳
                nonceStr = result.Noncestr, // 必填，生成签名的随机串
                signature = result.Signature// 必填，签名，见附录1
            });

            ViewBag.JSSDKConfig = sdkConfigStr;
            return View();
        }

    }
}