namespace LocalChat
{
    class GroupChatMessage: IGroupChatMessage
    {
        string _sender;
        string _message;

        public GroupChatMessage(string sender, string message)
        {
            _sender = sender;
            _message = message;
        }

        public string Sender {
            get => _sender;
        }
        public string Message {
            get => _message;
        }
    }
}
