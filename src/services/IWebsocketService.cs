using System;

namespace LocalChat
{
    interface IWebsocketService: IObservable<IChatServiceSignal>
    {
        Boolean Connect();
        void Send(string message);
        void Close();
    }
}
