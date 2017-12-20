using System;
using System.Web.Mvc;
using WeChat.Application;
using WeChat.Dev.Models;
using WeChat.Domain.AggregatesModel;
using WeChat.Domain.SeedWork;
using Zap.WeChat.SDK.Cache;

namespace WeChat.Dev.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        IUnitOfWork _unitOfWork;
        IAccountService _accountService;

        public AccountController(IAccountService accountService, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _accountService = accountService;
        }

        #region 账号密码登录

        /// <summary>
        /// 账号密码登录
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <param name="workUserId"></param>
        /// <param name="appcode"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl = null, string workUserId = null, string appcode = null)
        {
            var model = new AccountModel { ReturnUrl = returnUrl, WorkUserId = workUserId };
            if (workUserId == null)
            {
                return RedirectToAction("error", "home", new { errmsg = "无效的参数workUserId" });
            }
            if (appcode == null)
            {
                return RedirectToAction("error", "home", new { errmsg = "无效的参数appcode" });
            }
            ViewBag.AppCode = appcode;
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(AccountModel model)
        {
            var errmsg = string.Empty;
            var token = string.Empty;
            var errcode = 0;
            try
            {
                if (string.IsNullOrEmpty(model.WorkUserId))
                    throw new ArgumentNullException(nameof(model.WorkUserId));
                var user = _accountService.Login(model.Account, model.Password);
                if (user != null)
                {
                    //业务系统绑定微信号
                    _accountService.BindWorkID(user.UserId, model.WorkUserId);
                    //根据OAuth2.0 颁发令牌
                    token = user.UserId + user.WorkUserId;
                    _unitOfWork.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                errcode = 500;
                errmsg = ex.Message;
            }
            return Json(new { errmsg, errcode, token });
        }

        #endregion

        #region 微信身份登录

        /// <summary>
        /// 微信静默登录
        /// </summary>
        /// <param name="nonce"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult WorkLogin(string nonce, string returnUrl = null, string appcode = null)
        {
            var user = LocalCacheManager.Get<User>(nonce);
            try
            {
                if (user == null)
                {
                    throw new Exception("nonce 无效或已过期");
                }
                if (string.IsNullOrEmpty(user.WorkUserId))
                {
                    throw new ArgumentNullException(nameof(user.WorkUserId));
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("error", "home", new { errmsg = ex.Message });
            }
            ViewBag.AppCode = appcode;
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.Nonce = nonce;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult WorkLogin(string nonce)
        {
            var errmsg = string.Empty;
            var token = string.Empty;
            var errcode = 0;
            try
            {
                var user = LocalCacheManager.Get<User>(nonce);
                if (user == null)
                {
                    throw new Exception("nonce 无效或已过期");
                }
                LocalCacheManager.Remove(nonce);
                if (string.IsNullOrEmpty(user.WorkUserId))
                {
                    //记录日志
                    throw new Exception("用户未绑定业务系统账号，请联系管理员");
                }
                //根据OAuth2.0 颁发令牌
                token = user.UserId + user.WorkUserId;
            }
            catch (Exception ex)
            {
                errcode = 500;
                errmsg = ex.Message;
            }
            return Json(new { errmsg, errcode, token });
        }

        #endregion
    }
}