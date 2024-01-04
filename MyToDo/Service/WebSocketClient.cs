using MyToDo.Common.Models;
using MyToDo.Extensions;
using Prism.Events;
using System.Text.Json;
using System.Windows;
using WebSocketSharp;

namespace MyToDo.Service
{
    internal class WebSocketClient(IEventAggregator aggregator)
    {
        private readonly IEventAggregator aggregator = aggregator;
        private WebSocket? ws;

        public static bool IsNeedToClose = false;

        public async void Init(string token)
        {
            // WebSocket 地址
            string url = "ws://192.168.3.219:18989/websocket";

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
                    var msg = JsonSerializer.Deserialize<WebsocketMsg>(e.Data);
                    switch(msg!.Content)
                    {
                        case "new":
                            {
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    aggregator.SendToast(new Common.Events.ToastModel()
                                    {
                                        Title = "你有新的事情啦！",
                                        Message = $"\n标题：{msg.Title}"
                                    });
                                });
                                break;
                            }
                        case "overdue":
                            {
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    aggregator.SendToast(new Common.Events.ToastModel()
                                    {
                                        Title = "任务截至啦!",
                                        Message = $"标题为 {msg.Title}\n的任务已到期！"
                                    });
                                });
                                break;
                            }
                    }
                    
                }
                catch (Exception)
                {
                    throw;
                }
            };

            // 在关闭连接时的事件处理
            ws.OnClose += async(sender, e) =>
            {
                if (e.Reason.Trim() == "An exception has occurred while receiving.")
                {
                    // 服务端WebSocket连接异常，反复尝试重连，直到连接成功
                    while (true)
                    {
                        await Task.Delay(1000);
                        ws.Connect();
                        if (ws.ReadyState == WebSocketState.Open)
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("WebSocket closed");
                }

            };

            // 在发生错误时的事件处理
            ws.OnError += (sender, e) =>
            {
                Console.WriteLine($"WebSocket error: {e.Message}");
            };

            // 启动 WebSocket 连接
            ws.Connect();
            
            IsNeedToClose = false;
            // 阻塞主线程，保持 WebSocket 连接
            while (!IsNeedToClose)
            {
                await Task.Delay(1000); // 可以根据需要调整延迟
            }
            ws.Close(CloseStatusCode.Normal, "Connection closed by the client");
            
        }

    }
}
