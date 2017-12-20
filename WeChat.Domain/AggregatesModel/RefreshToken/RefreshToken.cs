using WeChat.Domain.SeedWork;

namespace WeChat.Domain.AggregatesModel
{
    public class RefreshToken : Entity, IAggregateRoot
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? IssuedUtc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? ExpiresUtc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProtectedTicket { get; set; }
    }
}
