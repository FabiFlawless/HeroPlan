using System.Windows;
using System.Windows.Controls;

namespace HeroPlan
{
    /// <summary>
    /// Interaction logic for UserRegistrationWindow.xaml
    /// </summary>
    public partial class UserRegistrationWindow : Window
    {
        private readonly DataService _dataService;

        /// <summary>
        /// Initializes a new instance of the UserRegistrationWindow.
        /// </summary>
        /// <param name="dataService">The data service for user operations.</param>
        public UserRegistrationWindow(DataService dataService)
        {
            InitializeComponent();
            _dataService = dataService;
        }

        /// <summary>
        /// Handles the click event of the register button.
        /// Attempts to register a new user with the provided username and password.
        /// </summary>
        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text;
            var password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter a username and password.");
                return;
            }

            try
            {
                await _dataService.AddUserAsync(username, password);
                MessageBox.Show("User successfully registered.");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during registration: {ex.Message}");
            }
        }
    }
}