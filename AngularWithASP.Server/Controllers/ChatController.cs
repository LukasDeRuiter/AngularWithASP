using AngularWithASP.Server.DTOs;
using AngularWithASP.Server.Models;
using AngularWithASP.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace AngularWithASP.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ChatApplicationServices _chatApplicationServices;

        public ChatController(ChatApplicationServices chatApplicationServices) {
            _chatApplicationServices = chatApplicationServices;
        }

        [HttpGet]
        public IEnumerable<Message> Get()
        {
            return _chatApplicationServices.GetChatMessages();
        }

        [HttpPost]
        public async Task<ActionResult<Message>> Post([FromBody] InputDTO input)
        {
            var aiMessage = await _chatApplicationServices.SendMessage(input);

            return Ok(aiMessage);
        }
    }
}
