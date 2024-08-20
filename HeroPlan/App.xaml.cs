using System.Windows;

namespace HeroPlan;
/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Initialize Application
    /// </summary>
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var dataService = new DataService();
        var loginWindow = new LoginWindow(dataService);
        loginWindow.Show();
    }
}