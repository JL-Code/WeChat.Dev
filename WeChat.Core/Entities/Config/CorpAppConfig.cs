namespace Zap.WeChat.SDK.Entities
{
    public class CorpAppConfig : CorpConfig
    {
        /// <summary>
        /// 企业应用秘钥
        /// </summary>
        public string CorpSecret { get; set; }

        /// <summary>
        /// 应用ID
        /// </summary>
        public int AgentId { get; set; }

        /// <summary>
        /// 应用编码
        /// </summary>
        public string AppCode { get; set; }
    }
}
