using System.Windows;

namespace HeroPlan;

/// <summary>
/// Interaction logic for InputDialog.xaml
/// </summary>
public partial class InputDialog : Window
{
    /// <summary>
    /// Initializes a new instance of the InputDialog.
    /// </summary>
    /// <param name="owner">The owner window of this dialog.</param>
    /// <param name="title">The title of the dialog window.</param>
    /// <param name="prompt">The prompt text to display to the user.</param>
    /// <param name="defaultValue">The default value to display in the input box.</param>
    public InputDialog(Window owner, string title, string prompt, string defaultValue = "")
    {
        InitializeComponent();
        Owner = owner;
        Title = title;
        InputTextboxDescription.Text = prompt;
        InputTextBox.Text = defaultValue;
        InputTextBox.SelectAll(); // Selects all text if present
        InputTextBox.Focus(); // Sets focus to the text field
    }

    /// <summary>
    /// Gets the input value entered by the user.
    /// </summary>
    public string Input { get; private set; }

    /// <summary>
    /// Handles the OK button click event.
    /// Sets the Input property and closes the dialog with a true result.
    /// </summary>
    private void OkButton_Click(object sender, RoutedEventArgs e)
    {
        Input = InputTextBox.Text;
        DialogResult = true;
    }

    /// <summary>
    /// Handles the Cancel button click event.
    /// Closes the dialog with a false result.
    /// </summary>
    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }
}