﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using WeChat.Application;
using WeChat.Dev.Models;
using WeChat.Dev.OAuthProviders;
using WeChat.Domain.AggregatesModel;
using WeChat.Domain.SeedWork;
using WeChat.Infrastructure;
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
                return RedirectToAction("error", "home", new { errmsg = "无效的参数workUserId", appcode });
            }
            if (appcode == null)
            {
                return RedirectToAction("error", "home", new { errmsg = "无效的参数appcode", appcode });
            }
            ViewBag.AppCode = appcode;
            ViewBag.WebSite = "http://meunsc.oicp.net";
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(AccountModel model)
        {
            var errmsg = string.Empty;
            OAuthModel token = null;
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
                    await _unitOfWork.SaveChangesAsync();
                    //根据OAuth2.0 颁发令牌
                    token = await GetTokenAsync(user);
                    if (token == null)
                    {
                        throw new Exception("令牌获取失败");
                    }
                }
            }
            catch (Exception ex)
            {
                errcode = 500;
                errmsg = ex.Message;
            }
            return Content(JsonHandler.ToJson(new { errmsg, errcode, token }));
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
                return RedirectToAction("error", "home", new { errmsg = ex.Message, appcode });
            }
            ViewBag.AppCode = appcode;
            ViewBag.WebSite = "http://meunsc.oicp.net";
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.Nonce = nonce;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> WorkLogin(string nonce)
        {
            var errmsg = string.Empty;
            OAuthModel token = null;
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
                token = await GetTokenAsync(user);
                if (token == null)
                {
                    throw new Exception("令牌获取失败");
                }
            }
            catch (Exception ex)
            {
                errcode = 500;
                errmsg = ex.Message;
            }

            return Content(JsonHandler.ToJson(new { errmsg, errcode, token }));
        }

        #endregion

        #region 获取授权令牌


        private async Task<OAuthModel> GetTokenAsync(User info)
        {
            //获取身份授权
            var http = new HttpKit("http://douhua.oicp.net");
            var content = new FormUrlEncodedContent(new[]
            {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("client_id", info.WorkUserId),
                    new KeyValuePair<string, string>("username", info.Account),
                    new KeyValuePair<string, string>("password", info.Password)
            });
            var resp = await http.PostAsync<OAuthModel>("/api/token", content);
            if (!resp.IsSuccess || string.IsNullOrEmpty(resp.Content.AccesssToken))
                return null;
            resp.Content.ExpiresIn = DateTime.Now.AddSeconds(resp.Content.ExpiresIn).ToJSTimeStamp();
            return resp.Content;
        }

        #endregion
    }
}