﻿using Zap.WeChat.SDK.AdvancedAPIs.AddressList;

namespace WeChat.Application
{
    public class MemberService : AccessTokenService, IMemberService
    {
        public MemberService(IWeChatAppService appService)
            : base(appService)
        {
        }

        public MemberResult GetMember(string appcode, string userId)
        {
            var app = GetToken(appcode);
            return AddressListApi.GetMember(app.AccessToken, userId);
        }
    }
}
