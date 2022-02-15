using System;
using System.Net.WebSockets;
using System.Threading;
using System.Text;
using System.Threading.Tasks;

namespace LocalChat
{
    class WebsocketClientService<ChatServiceSignal>: Observable<IChatServiceSignal>, IWebsocketService
    {
        #region private fields
        private ClientWebSocket wscli;
        private CancellationTokenSource tokSrc;
        private Task task;
        private readonly int _port;
        #endregion

        public WebsocketClientService(int port) {
            _port = port;
        }

        #region public methods
        public Boolean Connect()
        {
            try
            {
                wscli = new ClientWebSocket();
                tokSrc = new CancellationTokenSource();
                task = wscli.ConnectAsync(new Uri($"ws://127.0.0.1:{_port}"), tokSrc.Token);
                    
                task.Wait();
                task.Dispose();
                Recieve();
                return true;
            }
            catch (System.Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }

        public void Send(string message) {
            task = wscli.SendAsync(
                new ArraySegment<byte>(Encoding.UTF8.GetBytes(message)),
                WebSocketMessageType.Text,
                true,
                tokSrc.Token
            );
            task.Wait();
            task.Dispose();
        }
        
        public void Close()
        {
            if (wscli.State == WebSocketState.Open)
            {
                task = wscli.CloseAsync(WebSocketCloseStatus.NormalClosure, "", tokSrc.Token);
                task.Wait(); task.Dispose();
            }
            tokSrc.Dispose();
        }
        #endregion

        #region private methods
        private async void Recieve() {
            while (wscli.State == WebSocketState.Open)
            {
                var receiveBuffer = new byte[200];
                var offset = 0;
                var dataPerPacket = 10; //Just for example
                while (true)
                {
                    ArraySegment<byte> bytesReceived =
                                new ArraySegment<byte>(receiveBuffer, offset, dataPerPacket);
                    WebSocketReceiveResult result = await wscli.ReceiveAsync(bytesReceived, tokSrc.Token);
                    offset += result.Count;
                    if (result.EndOfMessage)
                        break;
                }
                
                IChatServiceSignal chatServiceSignal = (IChatServiceSignal)Activator.CreateInstance(typeof(ChatServiceSignal), new object[] { null, Encoding.UTF8.GetString(receiveBuffer, 0,
                                                offset) });
                Notify(chatServiceSignal);
            }
        }
        #endregion
    }
}
