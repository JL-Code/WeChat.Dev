using Senparc.Weixin.Work;
using Zap.WeChat.SDK.IServices;

namespace Zap.WeChat.SDK.Handler
{
    public class SignatureHandler : BaseService
    {
        public SignatureHandler(ICorpAppService service, string appcode) : base(service, appcode)
        {
        }

        public SignatureHandler(ICorpAppService service, int agentId) : base(service, agentId)
        {
        }

        public string VerifyURL(string msgSignature, string timeStamp, string nonce, string echoStr) =>
    Signature.VerifyURL(Config.Token, Config.EncodingAESKey, Config.CorpId, msgSignature, timeStamp, nonce, echoStr);

    }
}
