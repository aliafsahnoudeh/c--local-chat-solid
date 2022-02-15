using System;

namespace LocalChat
{
    class ViewCli: IView
    {
       
        IChatController _chatController;

        public ViewCli(IChatController chatController)
        {
            _chatController = chatController;
            _chatController.Output += OnChatOutput;
            _chatController.EnterData += OnEnterData;
            _chatController.StartReceiving += OnStartReceiving;
        }

        #region public methods

        public void Write(string data)
        {
            try
            {
                Console.WriteLine(data);
            }
            catch (Exception exeption)
            {
                ExceptionHandler.HandleException(exeption);
            }
        }

        public string Read()
        {
            try
            {
                return Console.ReadLine();
            }
            catch (Exception exeption)
            {
                ExceptionHandler.HandleException(exeption);
                return "";
            }
        }

        public void Start()
        {
            try
            {
                _chatController.Start();
            }
            catch (Exception exeption)
            {
                ExceptionHandler.HandleException(exeption);
            }
        }

        #endregion

        #region  private methods
        private string OnEnterData()
        {
            try
            {
                return Read();
            }
            catch (Exception exeption)
            {
                ExceptionHandler.HandleException(exeption);
                return "";
            }
        }

        private void OnChatOutput(string data)
        {
            try
            {
                Write(data);
            }
            catch (Exception exeption)
            {
                ExceptionHandler.HandleException(exeption);
            }
        }

        private void OnStartReceiving() 
        {
            try
            {
                ConsoleKeyInfo cki;
                var message = String.Empty;
                var terminate = false;
                do
                {
                    cki = Console.ReadKey(true);
                    Console.Write(cki.KeyChar);

                    if(cki.Key == ConsoleKey.Enter) {
                        _chatController.OnData(message);
                        message = String.Empty;
                        Write(String.Empty);
                    } else if(cki.Key != ConsoleKey.Escape)
                        message += cki.KeyChar;
                } while (!terminate && cki.Key != ConsoleKey.Escape);
                _chatController.End();
            }
            catch (Exception exeption)
            {
                ExceptionHandler.HandleException(exeption);
            }
        }
        #endregion
    }
}
