using AngularWithASP.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularWithASP.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
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
        public IEnumerable<Message> Post([FromBody] string userInput)
        {

            var messages = new List<Message>
            {
                new Message { Id = 0, Text = userInput, Position = 0 },
                new Message { Id = 1, Text = "Oof2", Position = 1 },
                new Message { Id = 2, Text = "Oof3", Position = 2 },
            };

            return messages;
        }
    }
}
