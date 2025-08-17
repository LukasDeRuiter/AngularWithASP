using AngularWithASP.Server.Data;
using AngularWithASP.Server.DTOs;
using AngularWithASP.Server.Models;
using AngularWithASP.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using OllamaSharp;
using Microsoft.Extensions.AI;

namespace AngularWithASP.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ChatContext _chatContext;
        private readonly ChatService _chatService;

        public ChatController(
            ChatContext chatContext,
            ChatService chatService
            ) {
            _chatContext = chatContext;
        }

        [HttpGet]
        public IEnumerable<Message> Get()
        {
            return _chatService.GetChatMessages();
        }

        [HttpPost]
        public async Task<ActionResult<Message>> Post([FromBody] InputDTO input)
        {
            var aiMessage = await _chatService.SendMessage(input);

            return Ok(aiMessage);
        }
    }
}
