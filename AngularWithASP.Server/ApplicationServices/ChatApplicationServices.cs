using AngularWithASP.Server.Data;
using AngularWithASP.Server.Models;
using Microsoft.EntityFrameworkCore;
using OllamaSharp;
using Microsoft.Extensions.AI;
using AngularWithASP.Server.DTOs;
using AngularWithASP.Server.DomainServices;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AngularWithASP.Server.Services
{
    public class ChatApplicationServices
    {
        private readonly ChatContext _chatContext;
        private readonly ChatDomainServices _chatDomainServices;

        public ChatApplicationServices(ChatContext chatContext, ChatDomainServices chatDomainServices)
        {
            _chatContext = chatContext;
            _chatDomainServices = chatDomainServices;
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
            var lastMessage = await _chatContext
                .Messages
                .OrderByDescending(message => message.Position)
                .FirstOrDefaultAsync();

            var newMessage = _chatDomainServices.FormatMessage(input.UserInput, _chatDomainServices.CalculateNewPosition(lastMessage));
            SaveMessage(newMessage);

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

            var aiMessage = _chatDomainServices.FormatMessage(aiResponse, newMessage.Position + 1);
            SaveMessage(aiMessage);

            return aiMessage;
        }

        private async void SaveMessage(Message message)
        {
            _chatContext.Messages.Add(message);
            await _chatContext.SaveChangesAsync();
        }
    }
}
