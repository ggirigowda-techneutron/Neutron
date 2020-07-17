using System.Windows;

namespace Presentation.Wpf
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        ///     Startup.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var app = new MainWindow();
            var context = new MainWindowViewModel();
            app.DataContext = context;
            app.Show();
        }
    }
}