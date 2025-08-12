using AngularWithASP.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularWithASP.Server.Data
{
    public class ChatContext : DbContext
    {
        public ChatContext(DbContextOptions options) : base(options)
        {
        }

        protected ChatContext()
        {
        }

        public DbSet<Message> Messages { get; set; }
    }
}
