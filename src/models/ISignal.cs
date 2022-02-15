using System;

namespace LocalChat
{
    interface ISignal
    {
        SignalType SignalType
        {
            get;
        }
        string Content
        {
            get;
        }
    }
}
