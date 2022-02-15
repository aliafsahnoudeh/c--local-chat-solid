using System;

namespace LocalChat
{
    class ChatServiceFactory<WebsocketClientService, WebsocketServerService, ChatClientService, ChatServerService>: IChatServiceFactory
    {
        #region private fields
        private IChatStateService _chatStateService;
        #endregion

        public ChatServiceFactory(IChatStateService chatStateService)
        {
            _chatStateService = chatStateService;
        }

        #region public methods
        public IChatService CreateNewChatService(int port) {
            IWebsocketService websocketClientService = (IWebsocketService)Activator.CreateInstance(typeof(WebsocketClientService), new object[] { port });
            if(websocketClientService.Connect())
                return (IChatService)Activator.CreateInstance(typeof(ChatClientService), new object[] { _chatStateService, websocketClientService });                
            
            IWebsocketService websocketServerService = (IWebsocketService)Activator.CreateInstance(typeof(WebsocketServerService), new object[] { port, ChatStateService.Instance });
            websocketServerService.Connect();
            IChatService chatService = (IChatService)Activator.CreateInstance(typeof(ChatServerService), new object[] { _chatStateService, websocketServerService });
            return chatService;
        }
        #endregion
    }
}
