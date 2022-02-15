namespace LocalChat
{
    public delegate void MessageReceivedDelegate(string message);

    interface IChatService: IObserver<IChatServiceSignal>
    {
        event MessageReceivedDelegate MessageReceived;
        void Start(string name);
        void End();
        void SendGroupMessage(string message);
    }
}
