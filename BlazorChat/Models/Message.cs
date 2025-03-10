namespace BlazorChat.Models
{
    public class Message
    {
        public string Username { get; set; }
        public string Body { get; set; }
        public bool IsMine { get; set; }

        public Message(string username, string body, bool isMine)
        {
            Username = username;
            Body = body;
            IsMine = isMine;
        }
    }
}