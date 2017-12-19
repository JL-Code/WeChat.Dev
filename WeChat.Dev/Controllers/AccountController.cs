using System.Web.Mvc;
using WeChat.Application;
using WeChat.Dev.Models;
using Zap.WeChat.SDK.Cache;

namespace WeChat.Dev.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        #region 账号密码登录

        [AllowAnonymous]
        public ActionResult Login(string returnUrl, string wxuserid)
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(AccountModel model)
        {
            ValidateModel(model);

            return View();
        }

        #endregion

        #region 微信身份登录

        public ActionResult WorkLogin(string nonce, string returnUrl)
        {
            var wxuserid = LocalCacheManager.Get<string>(nonce);
            if (string.IsNullOrEmpty(wxuserid))
            {

            }
            return View();
        }

        #endregion
    }
}