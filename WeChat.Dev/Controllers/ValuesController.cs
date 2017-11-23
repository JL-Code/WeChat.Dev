using System.Web.Http;
using WeChat.Core;

namespace WeChat.Dev.Controllers
{
    [RoutePrefix("api/messages")]
    public class MessageController : ApiController
    {

        IWeChatAppService _currentService;
        IMessageService _msgService;
        public MessageController(IWeChatAppService currentService, IMessageService msgService)
        {
            _currentService = currentService;
            _msgService = msgService;
        }
        [Route("")]
        [HttpGet]
        public IHttpActionResult Text(string appcode, string message, string toUser)
        {
            _msgService.SendText(appcode, message, toUser);
            return Ok();
        }
    }
}
