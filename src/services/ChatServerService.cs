using System;
using Newtonsoft.Json;

namespace LocalChat
{
    class ChatServerService<GroupChatMessage>: IChatService
    {
        #region public fields
        public event MessageReceivedDelegate MessageReceived;
        #endregion
        
        #region private fields
        private IWebsocketService _websocketService;
        private IChatStateService _chatStateService;
        private string _myName;
        #endregion

        public ChatServerService(IChatStateService chatStateService, IWebsocketService websocketService)
        {
            _chatStateService = chatStateService;
            _websocketService = websocketService;
            _websocketService.Attach(this);
        }

        #region  public methods
        public void Start(string name) {
            _myName = name;
        }

        public void End()
        {
            _websocketService.Close();
        }

        public void SendGroupMessage(string message) {
            // if server, can safely add it's username
            IGroupChatMessage groupChatMessage = (IGroupChatMessage)Activator.CreateInstance(typeof(GroupChatMessage), new object[] { _myName, message });
            var signal = new Signal(SignalType.GroupChatMessage, JsonConvert.SerializeObject(groupChatMessage));
            _websocketService.Send(JsonConvert.SerializeObject(signal));
        }

        public void Update(IChatServiceSignal data)
        {
            var signal = JsonConvert.DeserializeObject<Signal>(data.Content);
            if (signal.SignalType == SignalType.UserName)
            {
                _chatStateService.UpdateUser(data.Socket, signal.Content);
                _websocketService.Send(JsonConvert.SerializeObject(new Signal(SignalType.UserJoined, signal.Content)));
                    MessageReceived($"{signal.Content} joind the chat!");
            }
            else if(signal.SignalType == SignalType.GroupChatMessage)
            {
                // Add the name of the user to message and broadcast it.
                var username = _chatStateService.GetUsername(data.Socket);
                IGroupChatMessage groupChatMessage = (IGroupChatMessage)Activator.CreateInstance(typeof(GroupChatMessage), new object[] { username, signal.Content });
                var newContent = JsonConvert.SerializeObject(groupChatMessage);
                var newSignal = new Signal(signal.SignalType, newContent);
                _websocketService.Send(JsonConvert.SerializeObject(newSignal));
                MessageReceived($"{username}: {signal.Content}");
            } 
            else if (signal.SignalType == SignalType.UserLeft)
            {
                var name = _chatStateService.RemoveUser(data.Socket);
                    MessageReceived($"{name} left the chat!");
                    _websocketService.Send(JsonConvert.SerializeObject(new Signal(SignalType.UserLeft, name)));
            }
        }
        #endregion
    }
}
