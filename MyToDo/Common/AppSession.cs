using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MyToDo.Common
{
    public static class AppSession
    {
        public static string Token { get; set; } = "";

        public static string Username { get; set; } = "";

        public static ITheme Theme { get; set; }

        public static Color ThemeColor { get; set; }

    }
}
