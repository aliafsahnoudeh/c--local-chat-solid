namespace LocalChat
{
    class ChatController: IChatController
    {
        #region delegates & events
        public event OutputDelegate Output;
        public event EnterDelegate EnterData;
        public event StartReceivingDelegate StartReceiving;
        #endregion

        private IChatServiceFactory _chatServiceFactory;
        private IChatService _chatService;

        public ChatController(IChatServiceFactory chatServiceFactory)
        {
            _chatServiceFactory = chatServiceFactory;
        }

        #region  public methods
        public void Start()
        {
            // TODO prevent using hardcoded strings
            Output("Please enter a port number");
            var port = EnterData();
             // TODO validation for port value

            _chatService = _chatServiceFactory.CreateNewChatService(int.Parse(port));
            _chatService.MessageReceived += OnMessageReceived;
            Output("Please enter your name");
            var name = EnterData();
            _chatService.Start(name);
            Output("You're in, enjoy ...");
            Output("By pressing ESC button, your chat will end!");
            StartReceiving();
        }

        public void End()
        {
            _chatService.End();
        }

        public void OnData(string data)
        {
            _chatService.SendGroupMessage(data);
        }
        #endregion

        #region  private methods
        private void OnMessageReceived(string message)
        {
            Output(message);
        }
        #endregion
    }
}
