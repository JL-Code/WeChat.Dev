using Newtonsoft.Json;

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

    public class CreateDepartmentResult : WeChatResponse
    {
        /// <summary>
        /// 创建的部门id
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }
    }
}
