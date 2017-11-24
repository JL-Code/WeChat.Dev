using Senparc.Weixin.Work.AdvancedAPIs.MailList;
using Senparc.Weixin.Work.AdvancedAPIs.Mass;
using System;
using System.Linq;
using Zap.WeChat.SDK.AdvancedAPIs.AddressList;
using Zap.WeChat.SDK.MessageAPI;

namespace Zap.WeChat.SDK
{
    public static class ResultExtends
    {
        public static MessageResult ToMsgResult(this MassResult result)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));
            return new MessageResult
            {
                ErrorCode = result.ErrorCodeValue,
                ErrorMessage = result.errmsg,
                InvalidParty = result.invalidparty,
                InvalidTag = result.invalidtag,
                InvalidUser = result.invaliduser
            };
        }

        public static MemberResult ToMemberResult(this GetMemberResult result)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));
            return new MemberResult
            {
                ErrorCode = result.ErrorCodeValue,
                ErrorMessage = result.errmsg,
                UserID = result.userid,
                Name = result.name,
                Department = result.department,
                Order = result.order,
                Position = result.position,
                Mobile = result.mobile,
                Gender = result.gender,
                Email = result.email,
                IsLeader = result.isleader,
                Avatar = result.avatar,
                TelePhone = result.telephone,
                EnglishName = result.english_name,
                Extattr = new AdvancedAPIs.AddressList.Extattr
                {
                    Attrs = result.extattr.attrs.Select(m => new AdvancedAPIs.AddressList.Attr
                    {
                        Name = m.name,
                        Value = m.value
                    }).ToList()
                },
                Status = result.status
            };
        }
    }
}
