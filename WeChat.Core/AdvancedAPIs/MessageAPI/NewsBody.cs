using Newtonsoft.Json;
using System.Collections.Generic;

namespace Zap.WeChat.SDK.MessageAPI
{
    /// <summary>
    /// 图文消息主体
    /// </summary>
    public class NewsBody
    {
        /// <summary>
        /// 文章列表
        /// </summary>
        [JsonProperty("articles")]
        public List<Article> Articles { get; set; }
    }

    /// <summary>
    /// 文章
    /// </summary>
    public class Article
    {

        /// <summary>
        /// 文章标题，不超过128个字节 
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }
        /// <summary>
        /// 描述，不超过512个字节 
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
        /// <summary>
        /// 点击后跳转的链接
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }
        /// <summary>
        /// 图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80。如不填，在客户端不显示图片
        /// </summary>
        [JsonProperty("picurl")]
        public string PicUrl { get; set; }
    }
}
