using System.Collections.Generic;
using System.Linq;
using Fleck;

namespace LocalChat
{
    // It's been used by server to keep the chat state
    // TODO we can to keep the chat history
    class ChatStateService: IChatStateService
    {
        #region private fields
       private static ChatStateService _instance = null;
       private readonly List<User> _users;
       #endregion

       private ChatStateService()
       {
           _users = new List<User>();
       }

       public static ChatStateService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ChatStateService();
                }
                return _instance;
            }
        }

       #region public methods
       public void AddUser(IWebSocketConnection socket)
       {
           _users.Add(new User(socket, ""));
       }

       public string RemoveUser(IWebSocketConnection socket)
       {
           var user = GetUserBySocket(socket);
           var name = user.Name;
           _users.Remove(user);
           return name;
       }

       public void UpdateUser(IWebSocketConnection socket, string name)
       {
           // TODO check for existed username
           var user = GetUserBySocket(socket);
           user.Name = name;
       }

       public List<IWebSocketConnection> GetSockets()
       {
           var sockets = new List<IWebSocketConnection>();
           // TODO improve this calss to prevent an extra iteration
           foreach(User user in _users) {
                sockets.Add(user.socket);
           }
           return sockets;
       }

       public string GetUsername(IWebSocketConnection socket)
       {
           var user = _users.FirstOrDefault(user => user.socket == socket);
           return user != null ? user.Name : "";
       }
       #endregion

       #region private methods
       private User GetUserBySocket(IWebSocketConnection socket)
       {
           return _users.FirstOrDefault(user => user.socket.ConnectionInfo.Id == socket.ConnectionInfo.Id);
       }
       #endregion
    }
}
