using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zap.WeChat.SDK.AdvancedAPIs.AddressList
{
    public class DepartmentResult : WeChatResponse
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 部门父级ID
        /// </summary>
        [JsonProperty("parentId")]
        public int ParentId { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        [JsonProperty("order")]
        public int Order { get; set; }
    }
}
