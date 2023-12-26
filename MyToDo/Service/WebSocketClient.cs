using MyToDo.Extensions;
using Prism.Events;
using System.Windows;
using WebSocketSharp;

namespace MyToDo.Service
{
    internal class WebSocketClient(IEventAggregator aggregator)
    {
        private readonly IEventAggregator aggregator = aggregator;
        private WebSocket? ws;

        public async void Init(string token)
        {
            // WebSocket 地址
            string url = "ws://127.0.0.1:8989/websocket";

            // 创建 WebSocket 客户端
            ws = new WebSocket(url);
            // 在建立连接前设置请求头

            ws.SetCookie(new WebSocketSharp.Net.Cookie
            {
                Name = "Authorization",
                Value = token
            });

            // 在连接建立时的事件处理
            ws.OnOpen += (sender, e) =>
            {
                Console.WriteLine("WebSocket opened");
            };

            // 在接收到消息时的事件处理
            ws.OnMessage += (sender, e) =>
            {
                try
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        aggregator.SendToast(new Common.Events.ToastModel()
                        {
                            Title = "骑鲸协同",
                            Message = $"标题: {e.Data}\n已到期！"
                        });
                    });
                }
                catch (Exception)
                {
                    throw;
                }
            };

            // 在关闭连接时的事件处理
            ws.OnClose += (sender, e) =>
            {
                Console.WriteLine("WebSocket closed");
            };

            // 在发生错误时的事件处理
            ws.OnError += (sender, e) =>
            {
                Console.WriteLine($"WebSocket error: {e.Message}");
            };

            // 启动 WebSocket 连接
            ws.Connect();

            // 阻塞主线程，保持 WebSocket 连接
            while (ws.ReadyState == WebSocketState.Open)
            {
                await Task.Delay(1000); // 可以根据需要调整延迟
            }
        }

        public void Disconnect()
        {
            ws?.Close();
        }
    }
}
