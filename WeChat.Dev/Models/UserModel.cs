namespace WeChat.Dev.Models
{
    /// <summary>
    /// 登录
    /// </summary>
    public class AccountModel
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 跳转URL
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// 微信用户ID
        /// </summary>
        public string WxUserId { get; set; }
    }
}