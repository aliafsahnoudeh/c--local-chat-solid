using Fleck;

namespace LocalChat
{
    class ChatServiceSignal: IChatServiceSignal
    {
        IWebSocketConnection _socket;
        string _content;

        public ChatServiceSignal(IWebSocketConnection socket, string content)
        {
            _socket = socket;
            _content = content;
        }

        public IWebSocketConnection Socket {
            get => _socket;
        }
        public string Content {
            get => _content;
        }
    }
}
