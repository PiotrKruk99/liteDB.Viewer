using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls;

namespace LiteDBViewer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public IClassicDesktopStyleApplicationLifetime _appLifetime;

        public MainWindowViewModel()
        {
            _appLifetime = (App.Current!.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)!;
        }

        public void OnExitClick()
        {
            _appLifetime.Shutdown();
        }
    }
}
