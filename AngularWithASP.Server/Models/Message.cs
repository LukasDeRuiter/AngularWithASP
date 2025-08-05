namespace AngularWithASP.Server.Models
{
    public class Message
    {
        public int Id { get; set; }
        public required string Text { get; set; }
        public int Position { get; set; }
    }
}
