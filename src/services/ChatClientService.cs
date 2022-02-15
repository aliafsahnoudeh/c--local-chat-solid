using System;
using Newtonsoft.Json;

namespace LocalChat
{
    class ChatClientService<GroupChatMessage>: IChatService
    {
        #region private fields
        public event MessageReceivedDelegate MessageReceived;
        private IWebsocketService _websocketService;
        private IChatStateService _chatStateService;
        private string _myName;
        #endregion

        public ChatClientService(IChatStateService chatStateService, IWebsocketService websocketService)
        {
            _chatStateService = chatStateService;
            _websocketService = websocketService;
            _websocketService.Attach(this);
        }

        #region public methods
        public void Start(string name) {
            _myName = name;
            var signal = new Signal(SignalType.UserName, name);
            string output = JsonConvert.SerializeObject(signal);
            _websocketService.Send(output);
        }

        public void SendGroupMessage(string message) {
            Signal signal = null;
            signal = new Signal(SignalType.GroupChatMessage, message);
            _websocketService.Send(JsonConvert.SerializeObject(signal));
        }

        public void Update(IChatServiceSignal data)
        {
            var signal = JsonConvert.DeserializeObject<Signal>(data.Content);
            if(signal.SignalType == SignalType.GroupChatMessage)
            {
                IGroupChatMessage groupChatMessage = (IGroupChatMessage)JsonConvert.DeserializeObject<GroupChatMessage>(signal.Content);
                // TODO improve making desicion to show the message or not
                if(groupChatMessage.Sender != _myName)
                    MessageReceived($"{groupChatMessage.Sender}: {groupChatMessage.Message}");
            }
            if(signal.SignalType == SignalType.UserJoined && signal.Content != _myName)
                MessageReceived($"{signal.Content} joind the chat!");
            if(signal.SignalType == SignalType.UserLeft)
                MessageReceived($"{signal.Content} left the chat!");
        }

        public void End()
        {
            _websocketService.Close();
        }
        #endregion
    }
}
