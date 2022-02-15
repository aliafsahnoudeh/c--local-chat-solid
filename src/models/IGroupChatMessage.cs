namespace LocalChat
{
    interface IGroupChatMessage
    {
        string Sender {
            get;
        }
        string Message {
            get;
        }
    }
}
