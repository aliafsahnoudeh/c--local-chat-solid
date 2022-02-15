namespace LocalChat
{
    public delegate void OutputDelegate(string data);
    public delegate string EnterDelegate();
    public delegate void StartReceivingDelegate();

    interface IChatController
    {
        event OutputDelegate Output;
        event EnterDelegate EnterData;
        event StartReceivingDelegate StartReceiving;
        void Start();
        void End();
        void OnData(string data);
    }
}
