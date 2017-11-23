using Senparc.Weixin.Work.AdvancedAPIs.Mass;

namespace WeChat.Core.MessageAPI
{
    public static class ResultExtends
    {
        public static MessageResult ToMsgResult(this MassResult result)
        {
            if (result == null)
                return new MessageResult();
            return new MessageResult
            {
                ErrorCode = result.ErrorCodeValue,
                ErrorMessage = result.errmsg,
                InvalidParty = result.invalidparty,
                InvalidTag = result.invalidtag,
                InvalidUser = result.invaliduser
            };
        }
    }
}
