using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.Work;
using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.AdvancedAPIs.MailList;
using System.Collections.Generic;

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


        #endregion

        #region 标签管理


        #endregion

        #endregion
    }
}
