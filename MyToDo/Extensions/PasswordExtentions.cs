using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;

namespace MyToDo.Extensions
{
    public class PasswordExtentions
    {

        public static string GetPassword(DependencyObject obj)
        {
            return (string)obj.GetValue(PasswordProperty);
        }

        public static void SetPassword(DependencyObject obj, string value)
        {
            obj.SetValue(PasswordProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordExtentions), new PropertyMetadata(string.Empty,OnPasswordPropertyChanged));

        static void OnPasswordPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var passWord = sender as PasswordBox;
            string password = (string)e.NewValue;

            if(passWord != null && passWord.Password != password) {
                passWord.Password = password;
            }
        }

    }

    public class PasswordBehavior : Behavior<PasswordBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PasswordChanged += AssosiatedObject_PassowrdChanged;
        }

        private void AssosiatedObject_PassowrdChanged(object sender,RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            var password = PasswordExtentions.GetPassword(passwordBox);

            if(passwordBox != null && passwordBox.Password != password)
                PasswordExtentions.SetPassword(passwordBox, passwordBox.Password);
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PasswordChanged -= AssosiatedObject_PassowrdChanged;
        }
    }
}
