using Fleck;

namespace LocalChat
{
    interface IChatServiceSignal
    {
        IWebSocketConnection Socket {
            get;
        }
        string Content {
            get;
        }
    }
}
