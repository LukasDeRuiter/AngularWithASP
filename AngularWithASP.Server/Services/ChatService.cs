using AngularWithASP.Server.Data;
using AngularWithASP.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularWithASP.Server.Services
{
    public class ChatService
    {
        private readonly ChatContext _chatContext;

        public ChatService(ChatContext chatContext)
        {
            _chatContext = chatContext;
        }

        public List<Message> GetChatMessages()
        {
            var messages = _chatContext.Messages
                .OrderBy(m => m.Position)
                .ToList();

            return messages;
        }
    }
}
