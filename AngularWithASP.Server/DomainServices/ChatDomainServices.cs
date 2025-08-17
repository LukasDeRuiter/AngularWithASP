using AngularWithASP.Server.Models;

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
    }
}
