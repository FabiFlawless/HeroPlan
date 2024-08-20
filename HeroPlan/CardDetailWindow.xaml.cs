using System.Windows;

namespace HeroPlan;

/// <summary>
/// Interaction logic for CardDetailWindow.xaml
/// </summary>
public partial class CardDetailWindow : Window
{
    private readonly HeroTask _task;

    /// <summary>
    /// Initializes a new instance of the CardDetailWindow.
    /// </summary>
    /// <param name="task">The task to be displayed and edited.</param>
    public CardDetailWindow(HeroTask task)
    {
        InitializeComponent();
        _task = task;
        DataContext = _task;
        CardNameTextBlock.Text = task.Name;
        DescriptionTextBox.Text = task.Description;
        DeadlineDatePicker.SelectedDate = task.Deadline;
    }

    /// <summary>
    /// Handles the click event of the Save button.
    /// Updates the task with the edited information.
    /// </summary>
    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        _task.Description = DescriptionTextBox.Text;
        _task.Deadline = DeadlineDatePicker.SelectedDate;
        DialogResult = true;
    }

    /// <summary>
    /// Handles the click event of the Clear button for the deadline.
    /// Removes the currently set deadline.
    /// </summary>
    private void ClearDeadline_Click(object sender, RoutedEventArgs e)
    {
        DeadlineDatePicker.SelectedDate = null;
    }
}