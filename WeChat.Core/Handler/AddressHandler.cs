using System.Collections.Generic;
using Zap.WeChat.SDK.AdvancedAPIs.AddressList;
using Zap.WeChat.SDK.IServices;

namespace Zap.WeChat.SDK.Handler
{
    public class AddressHandler : BaseService
    {
        public AddressHandler(ICorpAppService service, string appcode) : base(service, appcode)
        {
        }

        public AddressHandler(ICorpAppService service, int agentId) : base(service, agentId)
        {
        }

        /// <summary>
        /// 获取成员
        /// </summary>
        /// <param name="userId">员工UserID</param>
        /// <returns></returns>
        public MemberResult GetMember(string userId)
        {
            return AddressListApi.GetMember(AppKey, userId);
        }

        /// <summary>
        /// 获取部门成员
        /// </summary>
        /// <param name="departmentId">获取的部门id</param>
        /// <param name="fetchChild">1/0：是否递归获取子部门下面的成员</param>
        /// <param name="maxJsonLength">设置 JavaScriptSerializer 类接受的 JSON 字符串的最大长度</param>
        /// <remarks>
        /// 参数maxJsonLength：企业号通讯录扩容后，存在Json长度不够的情况。
        /// </remarks>
        /// <returns></returns>
        public List<MemberResult> ListDepartmentMemberInfo(long departmentId, int fetchChild, int? maxJsonLength = null)
        {
            return AddressListApi.ListDepartmentMemberInfo(AppKey, departmentId, fetchChild, maxJsonLength);
        }
    }
}
