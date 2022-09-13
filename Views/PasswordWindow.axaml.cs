using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;

namespace LiteDBViewer.Views
{
    public class PasswordWindow : Window
    {
        private TextBox passwordTB;

        public PasswordWindow()
        {
            AvaloniaXamlLoader.Load(this);

            #if DEBUG
            this.AttachDevTools();
            #endif

            passwordTB = this.FindControl<TextBox>("passwordTB");
        }

        public void OnOKClick(object sender, RoutedEventArgs e)
        {
            Close(passwordTB.Text);
        }

        public void OnCancelClick(object sender, RoutedEventArgs e)
        {
            Close(null);
        }
    }
}