using AngularWithASP.Server.Data;
using AngularWithASP.Server.Models;
using Microsoft.EntityFrameworkCore;
using OllamaSharp;
using Microsoft.Extensions.AI;
using AngularWithASP.Server.DTOs;

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

        public async Task<Message> SendMessage(InputDTO input)
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

            var chatHistory = (await _chatContext.Messages
                .OrderBy(m => m.Position)
                .ToListAsync())
                .Select(m => new ChatMessage(
                    m.Text == input.UserInput ? ChatRole.User : ChatRole.Assistant,
                    m.Text))
                .ToList();

            IChatClient chatClient = new OllamaApiClient(new Uri("http://localhost:11434/"), "phi3:mini");
            string aiResponse = "";
            await foreach (var update in chatClient.GetStreamingResponseAsync(chatHistory))
            {
                aiResponse += update.Text;
            }

            var aiMessage = new Message
            {
                Text = aiResponse,
                Position = newMessage.Position + 1
            };

            _chatContext.Messages.Add(aiMessage);
            await _chatContext.SaveChangesAsync();

            return aiMessage;
        }
    }
}
