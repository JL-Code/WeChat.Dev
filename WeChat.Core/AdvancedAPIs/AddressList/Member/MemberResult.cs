using Newtonsoft.Json;
using System.Collections.Generic;

namespace Zap.WeChat.SDK.AdvancedAPIs.AddressList
{
    public class MemberResult : WeChatResponse
    {
        /// <summary>
        /// 成员UserID。对应管理端的帐号
        /// </summary>
        [JsonProperty("userid")]
        public string UserID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 手机号码，第三方仅通讯录套件可获取
        /// </summary>
        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        /// <summary>
        /// 成员所属部门id列表
        /// </summary>
        [JsonProperty("department")]
        public long[] Department { get; set; }

        /// <summary>
        /// 部门内的排序值，默认为0。数量必须和department一致，数值越大排序越前面。值范围是[0, 2^32)
        /// </summary>
        [JsonProperty("order")]
        public int[] Order { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        [JsonProperty("position", NullValueHandling = NullValueHandling.Ignore)]
        public string Position { get; set; }

        /// <summary>
        /// 性别。0表示未定义，1表示男性，2表示女性 
        /// </summary>
        [JsonProperty("gender")]
        public int Gender { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// 上级字段，标识是否为上级。1是 0否
        /// </summary>
        [JsonProperty("isleader")]
        public int IsLeader { get; set; }

        /// <summary>
        ///  头像url。注：如果要获取小图将url最后的"/0"改成"/100"即可 (注：企业微信旧版为/64) 
        /// </summary>
        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        /// <summary>
        /// 成员头像的mediaid，通过多媒体接口上传图片获得的mediaid
        /// eg:2-G6nrLmr5EC3MNb_-zL1dDdzkd0p7cNliYu9V5w7o8K0
        /// </summary>
        [JsonProperty("avatar_mediaid", NullValueHandling = NullValueHandling.Ignore)]
        public string AvatarMediaid { get; set; }

        /// <summary>
        ///  座机。第三方仅通讯录套件可获取
        /// </summary>
        [JsonProperty("telephone")]
        public string TelePhone { get; set; }

        /// <summary>
        ///  英文名
        /// </summary>
        [JsonProperty("english_name")]
        public string EnglishName { get; set; }

        /// <summary>
        /// 启用/禁用成员，第三方不可获取。1表示启用成员，0表示禁用成员
        /// </summary>
        [JsonProperty("enable")]
        public int Enable { get; set; }

        /// <summary>
        /// 扩展属性，第三方仅通讯录套件可获取
        /// </summary>
        [JsonProperty("extattr")]
        public Extattr Extattr { get; set; }

        /// <summary>
        /// 激活状态: 1=已激活，2=已禁用，4=未激活。已激活代表已激活企业微信或已关注微信插件。未激活代表既未激活企业微信又未关注微信插件。
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }
    }

    public class Extattr
    {
        public List<Attr> Attrs { get; set; }
    }

    /// <summary>
    /// 自定义属性
    /// </summary>
    public class Attr
    {
        /// <summary>
        ///  名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///  值
        /// </summary>
        [JsonProperty("value")]
        public object Value { get; set; }
    }
}
