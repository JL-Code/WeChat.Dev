using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChat.Core
{
    /// <summary>
    /// 微信响应消息
    /// </summary>
    public abstract class WeChatResponse
    {
        #region Properties

        /// <summary>
        /// 存在错误
        /// </summary>
        public bool HasError { get { return ErrorCode != 0; } }

        /// <summary>
        /// 错误代码
        /// </summary>
        [JsonProperty("errcode", NullValueHandling = NullValueHandling.Ignore)]
        public int ErrorCode { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        [JsonProperty("errmsg", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorMessage { get; set; }

        #endregion

        #region Override

        public override string ToString()
        {
            if (HasError)
                return String.Format("errcode:{0}, errmsg:{1}", ErrorCode, ErrorMessage);
            return base.ToString();
        }

        #endregion
    }
}
