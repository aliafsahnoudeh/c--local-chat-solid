namespace LocalChat
{
    interface IChatServiceFactory
    {
        IChatService CreateNewChatService(int port);
    }
}
