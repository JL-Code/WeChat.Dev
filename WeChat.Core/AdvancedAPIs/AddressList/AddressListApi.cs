using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.Work;
using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.AdvancedAPIs.MailList;
using System.Collections.Generic;
using System;

namespace Zap.WeChat.SDK.AdvancedAPIs.AddressList
{
    public class AddressListApi
    {
        #region 同步方法

        #region 成员管理

        /// <summary>
        /// 获取成员
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="userId">员工UserID</param>
        /// <returns></returns>
        public static MemberResult GetMember(string accessTokenOrAppKey, string userId)
        {
            var result = MailListApi.GetMember(accessTokenOrAppKey, userId);
            return result.ToMemberResult();
        }

        /// <summary>
        /// 获取部门成员
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="departmentId">获取的部门id</param>
        /// <param name="fetchChild">1/0：是否递归获取子部门下面的成员</param>
        /// <param name="maxJsonLength">设置 JavaScriptSerializer 类接受的 JSON 字符串的最大长度</param>
        /// <remarks>
        /// 2016-04-16：Zeje添加参数maxJsonLength：企业号通讯录扩容后，存在Json长度不够的情况。
        /// </remarks>
        /// <returns></returns>
        public static List<MemberResult> ListDepartmentMemberInfo(string accessTokenOrAppKey, long departmentId, int fetchChild, int? maxJsonLength = null)
        {
            var result = MailListApi.GetDepartmentMemberInfo(accessTokenOrAppKey, departmentId, fetchChild, maxJsonLength);
            return result.ToDepartmentMemberInfoResult();
        }



        #endregion

        #region 部门管理

        /// <summary>
        /// 创建部门
        /// 系统应用须拥有父部门的管理权限。
        /// 注意，部门的最大层级为15层；部门总数不能超过3万个；每个部门下的节点不能超过3万个。建议保证创建的部门和对应部门成员是串行化处理。
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="name">部门名称。长度限制为1~64个字节，字符不能包括\:?”<>｜</param>
        /// <param name="parentId">父亲部门id。根部门id为1 </param>
        /// <param name="order">在父部门中的次序。从1开始，数字越大排序越靠后</param>
        /// <param name="id">部门ID。用指定部门ID新建部门，不指定此参数时，则自动生成</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static CreateDepartmentResult CreateDepartment(string accessTokenOrAppKey, string name, long parentId, int order = 1, long? id = null)
        {
            var result = MailListApi.CreateDepartment(accessTokenOrAppKey, name, parentId, order, id);
            return result.ToCreateDepartmentResult();
        }

        /// <summary>
        /// 更新部门【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="id">部门id</param>
        /// <param name="name">更新的部门名称。长度限制为0~64个字符。修改部门名称时指定该参数</param>
        /// <param name="parentId">父亲部门id。根部门id为1 </param>
        /// <param name="order">在父部门中的次序。从1开始，数字越大排序越靠后</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WeChatResponse UpdateDepartment(string accessTokenOrAppKey, long id, string name, long parentId, int order = 1)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除部门【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="id">部门id。（注：不能删除根部门；不能删除含有子部门、成员的部门）</param>
        /// <returns></returns>
        public static WeChatResponse DeleteDepartment(string accessTokenOrAppKey, long id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取部门列表【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="id">部门id。获取指定部门及其下的子部门。 如果不填，默认获取全量组织架构</param>
        /// <returns></returns>
        public static List<DepartmentResult> GetDepartmentList(string accessTokenOrAppKey, long? id = null)
        {
            var result = ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/department/list?access_token={0}", accessToken.AsUrlData());

                if (id.HasValue)
                {
                    url += string.Format("&id={0}", id.Value);
                }

                return Get.GetJson<GetDepartmentListResult>(url);
            }, accessTokenOrAppKey);
            return result.ToDepartmentResult();

        }

        #endregion

        #region 标签管理


        #endregion

        #endregion
    }
}
