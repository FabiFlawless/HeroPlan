namespace HeroPlan;
/// <summary>
///     Interaction logic for LoginWindow.xaml
/// </summary>
public partial class LoginWindow : Window
{
    private readonly DataService _dataService;

    /// <summary>
    ///     Initializes a new instance of the LoginWindow.
    /// </summary>
    /// <param name="dataService">The data service for user authentication.</param>
    public LoginWindow(DataService dataService)
    {
        InitializeComponent();
        _dataService = dataService;
    }

    /// <summary>
    ///     Handles the login button click event.
    ///     Attempts to authenticate the user and opens the main window if successful.
    /// </summary>
    private async void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        var username = UsernameTextBox.Text;
        var password = PasswordBox.Password;
        if (await _dataService.AuthenticateUserAsync(username, password))
        {
            var mainWindow = new MainWindow(_dataService);
            mainWindow.Show();
            Close();
        }
        else
        {
            MessageBox.Show("Invalid credentials");
        }
    }
}