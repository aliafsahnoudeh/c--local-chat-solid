using Autofac;

namespace LocalChat
{
    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            #region registering services
            var builder = new ContainerBuilder();
            builder.Register(c => ChatStateService.Instance).As<IChatStateService>();
            builder.Register(c => new ChatServiceFactory<WebsocketClientService<ChatServiceSignal>,
                                    WebsocketServerService<ChatServiceSignal>,
                                    ChatClientService<GroupChatMessage>,
                                    ChatServerService<GroupChatMessage>>(c.Resolve<IChatStateService>())).As<IChatServiceFactory>();
            builder.Register(c => new ChatController(c.Resolve<IChatServiceFactory>())).As<IChatController>();
            builder.Register(c => new ViewCli(c.Resolve<IChatController>())).As<IView>();
            Container = builder.Build();
            #endregion

            using (var scope = Container.BeginLifetimeScope())
            {
               var viewCli = scope.Resolve<IView>();
               viewCli.Start();
            }
        }
    }
}
