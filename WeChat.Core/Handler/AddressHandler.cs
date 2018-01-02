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

        #region Operation

        /// <summary>
        /// 创建部门
        /// 系统应用须拥有父部门的管理权限。
        /// 注意，部门的最大层级为15层；部门总数不能超过3万个；每个部门下的节点不能超过3万个。建议保证创建的部门和对应部门成员是串行化处理。
        /// </summary>
        /// <param name="name">部门名称。长度限制为1~64个字节，字符不能包括\:?”<>｜</param>
        /// <param name="parentId">父亲部门id。根部门id为1 </param>
        /// <param name="order">在父部门中的次序。从1开始，数字越大排序越靠后</param>
        /// <param name="id">部门ID。用指定部门ID新建部门，不指定此参数时，则自动生成</param>
        /// <returns></returns>
        public CreateDepartmentResult CreateDepartment(string name, long parentId, int order = 1, long? id = null)
        {
            return AddressListApi.CreateDepartment(AppKey, name, parentId, order, id);
        }

        #endregion
    }
}
