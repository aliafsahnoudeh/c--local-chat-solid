using System.Collections.Generic;
using Fleck;

namespace LocalChat
{
    interface IChatStateService
    {
        void AddUser(IWebSocketConnection socket);
        string RemoveUser(IWebSocketConnection socket);
        void UpdateUser(IWebSocketConnection socket, string name);
        List<IWebSocketConnection> GetSockets();
        string GetUsername(IWebSocketConnection socket);
    }
}
