using System;
using Fleck;
using Newtonsoft.Json;

namespace LocalChat
{
    class WebsocketServerService<ChatServiceSignal>: Observable<IChatServiceSignal>, IWebsocketService
    {
        #region private fileds
        private WebSocketServer _server;
        private readonly int _port;
        private ChatStateService _chatStateService;
        #endregion

        public WebsocketServerService(int port, ChatStateService chatStateService) {
            _chatStateService = chatStateService;
            _port = port;
        }

        #region public methods
        public Boolean Connect() 
        {
            try
            {
                _server = new WebSocketServer($"ws://127.0.0.1:{_port}");
                _server.Start(socket =>
                {
                    socket.OnOpen = () => {
                        _chatStateService.AddUser(socket);
                    };
                    socket.OnClose = () => {
                        // TODO remove the usage of JsonConvert here
                        IChatServiceSignal chatServiceSignal = (IChatServiceSignal)Activator.CreateInstance(typeof(ChatServiceSignal), new object[] { socket, JsonConvert.SerializeObject(new Signal(SignalType.UserLeft, "")) });
                        Notify(chatServiceSignal);
                    };
                    socket.OnMessage = message => {
                        IChatServiceSignal chatServiceSignal = (IChatServiceSignal)Activator.CreateInstance(typeof(ChatServiceSignal), new object[] { socket, message });
                        Notify(chatServiceSignal);
                    };
                });
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public void Send(string message) {
            var sockets = _chatStateService.GetSockets();
            foreach(IWebSocketConnection webSocketConnection in sockets)
            {
                webSocketConnection.Send(message);
            }
        }
        
        public void Close()
        {
            _server.Dispose();
        }
        #endregion
    }
}
