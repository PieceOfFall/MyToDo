using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Notification.Wpf.Constants;
using HandyControl.Data;
using System.Text;

namespace MyToDo.Extensions
{
    public class NotifyIcon : FrameworkElement, IDisposable
    {
        private string? _notificationTitle = string.Empty;

        private StringBuilder? _contentCollection = new();

        public static Action<string, string, NotifyIconInfoType>? ShowBalloonTip { get; set; } = null;

        public string ButtonSystemUrl { get; set; } = string.Empty;

        public NotifyIcon(string? title = null)
        {
            // 初始化通知标题
            _notificationTitle = title ?? _notificationTitle;

            // 同时显示最大数量
            NotificationConstants.NotificationsOverlayWindowMaxCount = 5;

            // 最小显示宽度
            NotificationConstants.MinWidth = 400d;

            // 最大显示宽度
            NotificationConstants.MaxWidth = 460d;

        }

        public NotifyIcon AppendContentText(string? text = null)
        {
            _contentCollection!.AppendLine(text ?? string.Empty);
            return this;
        }

        public void Show()
        {
            _contentCollection!.AppendLine(); // content 不能为空，否则通知发不出去
            ShowBalloonTip!(_notificationTitle!, _contentCollection.ToString(), NotifyIconInfoType.None);
        }

        public void Dispose()
        {
            _contentCollection!.Clear();

            _notificationTitle = null;
            _contentCollection = null;
        }

        private static ImageSource GetAppIcon()
        {
            try
            {
                return new BitmapImage(new Uri("/Images/favicon.ico"));
            }
            catch (Exception)
            {
                return new BitmapImage();
            }
        }

    }
}
