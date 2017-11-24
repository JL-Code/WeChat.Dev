using Zap.WeChat.SDK.AdvancedAPIs.AddressList;

namespace Zap.WeChat.SDK
{
    public interface IMemberService
    {
        /// <summary>
        /// 获取成员信息
        /// </summary>
        /// <param name="appcode">应用编码</param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        MemberResult GetMember(string appcode, string userId);

    }
}
