using System;
using WeChat.Domain.SeedWork;

namespace WeChat.Domain.AggregatesModel
{
    /// <summary>
    /// 接口配置类
    /// </summary>
    public class ApplicationConfig : Entity, IAggregateRoot
    {
        /// <summary>
        /// 配置GUID
        /// </summary>
        public Guid ConfigGUID { get; set; }
        /// <summary>
        /// 配置类型
        /// </summary>
        public string ConfigType { get; set; }
        /// <summary>
        /// 配置键
        /// </summary>
        public string ConfigKey { get; set; }
        /// <summary>
        /// 配置名称
        /// </summary>
        public string ConfigName { get; set; }
        /// <summary>
        /// 配置值
        /// </summary>
        public string ConfigValue { get; set; }
    }
}
