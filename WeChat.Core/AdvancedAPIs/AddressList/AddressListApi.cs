using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.Work;
using Senparc.Weixin.Work.AdvancedAPIs.MailList;

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
            var result = ApiHandlerWapper.TryCommonApi(accessToken =>
             {
                 var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/get?access_token={0}&userid={1}", accessToken.AsUrlData(), userId.AsUrlData());
                 return Get.GetJson<GetMemberResult>(url);
             }, accessTokenOrAppKey);
            return result.ToMemberResult();
        }

        #endregion

        #region 部门管理


        #endregion

        #region 标签管理


        #endregion

        #endregion
    }
}
