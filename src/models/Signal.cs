using System;

namespace LocalChat
{
    class Signal : ISignal
    {
        SignalType _signalType;
        string _content;

        public Signal(SignalType SignalType, string Content)
        {
            _signalType = SignalType;
            _content = Content;
        }

        public SignalType SignalType {
            get => _signalType;
        }
        public string Content {
            get => _content;
        }
    }
}
