using System.Web.Mvc;

namespace WeChat.Dev.Controllers
{
    public class BaseController : Controller
    {
        // 企业ID 服务AppID 
        string _corpid = "";
        public BaseController()
        {
            _corpid = "";
        }

        public string AppId { get; }
        public string CorpId
        {
            get
            {
                return _corpid;
            }
        }
    }
}