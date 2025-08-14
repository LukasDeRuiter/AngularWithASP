using AngularWithASP.Server.Data;
using AngularWithASP.Server.DTOs;
using AngularWithASP.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AngularWithASP.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ChatContext _chatContext;

        public ChatController(ChatContext chatContext)
        {
            _chatContext = chatContext;
        }

        [HttpGet]
        public IEnumerable<Message> Get()
        {
            var messages = new List<Message>
            {
                new Message { Id = 0, Text = "Oof1", Position = 0 },
                new Message { Id = 1, Text = "Oof2", Position = 1 },
                new Message { Id = 2, Text = "Oof3", Position = 2 },
            };

            return messages;
        }

        [HttpPost]
        public async Task<ActionResult<Message>> Post([FromBody] InputDTO input)
        {
            int currentPosition = 0;

            var lastMessage = await _chatContext
                .Messages
                .OrderByDescending(message => message.Position)
                .FirstOrDefaultAsync();

            if (lastMessage != null)
            {
                currentPosition = lastMessage.Position + 1;
            }

            var newMessage = new Message
            {
                Text = input.UserInput,
                Position = currentPosition
            };

            _chatContext.Messages.Add(newMessage);
            await _chatContext.SaveChangesAsync();

            return Ok(newMessage);
        }
    }
}
