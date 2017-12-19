using WeChat.Domain.SeedWork;

namespace WeChat.Domain.AggregatesModel
{
    public class User: Entity, IAggregateRoot
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public System.Guid UserId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 企业微信用户ID
        /// </summary>
        public string WorkUserId { get; set; }
    }
}
