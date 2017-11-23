using System.Web.Http;
using WeChat.Core;

namespace WeChat.Dev.Controllers
{
    [RoutePrefix("api/message")]
    public class MessageController : ApiController
    {

        IWeChatAppService _currentService;
        IMessageService _msgService;
        public MessageController(IWeChatAppService currentService, IMessageService msgService)
        {
            _currentService = currentService;
            _msgService = msgService;
        }

        [HttpPost]
        [Route("text")]
        public IHttpActionResult Text(string appcode, string message)
        {
            _msgService.SendText(appcode, message);
            return Ok();
        }
    }
}
