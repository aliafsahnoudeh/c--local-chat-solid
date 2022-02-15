using Fleck;

namespace LocalChat
{
    class User
    {
        IWebSocketConnection _socket;
        string _name;

        public User(IWebSocketConnection socket, string name)
        {
            _socket = socket;
            _name = name;
        }

        public IWebSocketConnection socket {
            get => _socket;
        }
        public string Name {
            get => _name;
            set => _name = value;
        }
    }
}
