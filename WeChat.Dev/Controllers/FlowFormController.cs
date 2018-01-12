using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using WeChat.Dev.Models;
using WeChat.Infrastructure;
using Zap.WeChat.SDK;
using Zap.WeChat.SDK.Handler;
using Zap.WeChat.SDK.IServices;

namespace WeChat.Dev.Controllers
{
    /// <summary>
    /// 流程表单
    /// </summary>
    public class FlowFormController : Controller
    {
        ICorpAppService _corpAppService;
        public FlowFormController(ICorpAppService corpAppService)
        {
            _corpAppService = corpAppService;
        }

        #region Views

        /// <summary>
        /// 流程表单详情
        /// </summary>
        /// <param name="instanceId">流程实例ID</param>
        /// <param name="nodeId">节点ID</param>
        /// <param name="nodeExtId">节点扩展ID</param>
        /// <param name="nodeExtType">节点扩展类型 默认为原节点</param>
        /// <returns></returns>
        public ActionResult Detail(Guid instanceId, Guid? nodeId, Guid? nodeExtId, int nodeExtType = 0)
        {
            var currentUrl = Request.Url.ToString().Replace(":8070", "");
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
            var model = InitMenus(instanceId);
            ViewBag.JSSDKConfig = sdkConfigStr;
            return View(model);
        }

        /// <summary>
        /// 流程处理
        /// </summary>
        /// <param name="instanceId">流程实例ID</param>
        /// <param name="suggestion">处理默认意见</param>
        /// <param name="handleType">处理类型</param>
        /// <param name="currentUrl">当前访问地址</param>
        /// <returns></returns>
        public ActionResult Handle(Guid instanceId,
            string suggestion,
            int handleType,
            int disagreeHandleType = -1)
        {
            var currentUrl = Request.Url.ToString().Replace(":8070", "");
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

            var extraStr = JsonHandler.ToJson(new
            {
                instanceId,
                suggestion,
                handleType,
                disagreeHandleType
            });

            ViewBag.Extra = extraStr;
            ViewBag.JSSDKConfig = sdkConfigStr;
            return View();

        }


        #endregion

        #region Data

        /// <summary>
        /// 获取下一个步骤的节点信息
        /// </summary>
        /// <param name="instanceId">流程实例ID</param>
        /// <param name="nextNodeType">默认 下一步骤</param>
        /// <returns></returns>
        public string GetNextNodes(Guid instanceId, int nextNodeType = 0)
        {
            var data = new
            {
                Auditors = new List<dynamic> { "蒋勇", "陈丽", "杜宋思仪" },
                StepName = "结束",
                StepHandMode = 1 //步骤处理模式
            };
            return JsonHandler.ToJson(data);
        }

        #endregion

        #region Others

        public NavModel<FlowHandleMenu> InitMenus(Guid instanceId)
        {
            var menus = GetFlowHandleBtnNames(instanceId);
            var model = new NavModel<FlowHandleMenu>();
            menus.ForEach(menu =>
            {
                if (menu.ClassList.Count == 0)
                {
                    menu.ClassList.Add("nav-btn_primary");
                }
                if (string.IsNullOrEmpty(menu.Url))
                {
                    menu.Url = "/flowform/handle";
                }
            });

            model.Menus = menus;
            return model;
        }

        public List<FlowHandleMenu> GetFlowHandleBtnNames(Guid instanceId)
        {
            //Initiate = "发起";
            //Agree = "同意";
            //AgainInitiate = "重新发起";
            //DisAgree = "不同意";
            //AnswerAssign = "回复协商";
            //Assign = "协商";
            //Cancel = "作废";
            //JointSignature = "会签";
            //Recall = "撤销";
            //Transfer = "授权交办";
            //Urge = "催办";

            var menus = new List<FlowHandleMenu>{
                 new FlowHandleMenu { Name = "取消", Back = true, Url = "javascript:window.history.back();", IsDisplay=true,ClassList=new List<string>{ "nav-btn_default"} },
                 new FlowHandleMenu { Name = "发起", HandleType = FlowHandleType.Begin,IsDisplay=false } ,
                 new FlowHandleMenu { Name = "重新发起", HandleType = FlowHandleType.AgainInitiate,IsDisplay=false } ,
                 new FlowHandleMenu {
                     Name = "不同意",
                     ClassList= new List<string>{"nav-btn_warn"},
                     HandleType = FlowHandleType.Disagree,
                     Extra = new { },
                     IsDisplay = true,
                     ButtonType = ButtonType.Button,
                     SubMenus = new List<FlowHandleMenu>{
                         new FlowHandleMenu{
                             Name = "打回到发起",
                             HandleType = FlowHandleType.Disagree,
                             Extra = new {DisagreeHandleType = 1 },
                             Url="/flowform/handle"
                         },
                         new FlowHandleMenu{
                             Name= "打回到上一步",
                             HandleType = FlowHandleType.Disagree,
                             Extra = new {DisagreeHandleType = 0 },
                             Url ="/flowform/handle"
                         },
                     }
                 } ,
                 new FlowHandleMenu {
                     Name = "同意",
                     HandleType = FlowHandleType.Agree,
                     IsDisplay =true,
                     Url = $"/flowform/handle?instanceId={instanceId}&handletype={(int)FlowHandleType.Agree}&suggestion="
                 } ,
                 new FlowHandleMenu { Name = "回复协商", HandleType = FlowHandleType.AnswerAssign,IsDisplay = false },
                 new FlowHandleMenu { Name = "会签", HandleType = FlowHandleType.JointSignature,IsDisplay = false },
                 new FlowHandleMenu { Name = "催办", HandleType = FlowHandleType.Urge ,IsDisplay = false },
                 new FlowHandleMenu { Name = "撤销", HandleType = FlowHandleType.Recall,IsDisplay = false,
                     ClassList= new List<string>{"nav-btn_warn"}},
                 new FlowHandleMenu {
                    Name = "更多",
                    Extra = new { },
                    IsDisplay = true,
                    ButtonType = ButtonType.Button,
                    SubMenus = new List<FlowHandleMenu> {
                        new FlowHandleMenu { Name = "授权交办", HandleType = FlowHandleType.Transfer,Url="/flowform/handle" },
                        new FlowHandleMenu { Name = "发起协商", HandleType = FlowHandleType.Assign,Url="/flowform/handle" } ,
                        new FlowHandleMenu {
                            Name = "作废",
                            HandleType = FlowHandleType.Cancel,
                            Url ="/flowform/handle",
                            Extra = new { NodeType = 6 }
                        },
                    }
                 }
            };

            return menus;
        }

        #endregion
    }
}