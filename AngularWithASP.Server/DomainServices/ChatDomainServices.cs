using AngularWithASP.Server.Models;
using Azure;

namespace AngularWithASP.Server.DomainServices
{
    public class ChatDomainServices
    {
        public int CalculateNewPosition(Message LastMessage)
        {
            if (LastMessage != null)
            {
                return LastMessage.Position + 1;
            }

            return 0;
        }

        public Message FormatMessage(string messageText, int position)
        {
            var message = new Message
            {
                Text = messageText,
                Position = position
            };

            return message;
        }
    }
}
