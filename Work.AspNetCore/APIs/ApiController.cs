using Microsoft.AspNetCore.Mvc;
using Senparc.Weixin.Work.Containers;

namespace Work.AspNetCore.APIs
{
    /// <summary>
    /// api 控制器基类
    /// </summary>
    [Produces("application/json")]
    [Route("api/ApiController")]
    public class ApiController : Controller
    {
        private readonly static string _corpId = "ww13e9492fe4b02153";
        private readonly static string _corpSecret = "dsq1HWzRkcJ27VxrC8gRsTXu17Kq39LlJIY5kk3w-g4";

        /// <summary>
        /// 生成应用key
        /// </summary>
        public string APP_KEY { get => AccessTokenContainer.BuildingKey(_corpId, _corpSecret); }
    }
}