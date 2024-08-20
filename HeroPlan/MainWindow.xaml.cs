using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HeroPlan;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly DataService _dataService;
    private List<Board?> boards;
    private Board? currentBoard;

    /// <summary>
    /// Initializes a new instance of the MainWindow.
    /// </summary>
    public MainWindow(DataService dataService)
    {
        InitializeComponent();
        _dataService = dataService;
        InitializeAsync();
        UpdateUserManagementButtonVisibility();
    }

    /// <summary>
    /// Updates the visibility of the user management button based on admin status.
    /// </summary>
    private void UpdateUserManagementButtonVisibility()
    {
        UserManagementButton.Visibility = _dataService.IsAdminUser() ? Visibility.Visible : Visibility.Collapsed;
    }

    /// <summary>
    /// Handles the click event for the user management button.
    /// </summary>
    private void UserManagementButton_Click(object sender, RoutedEventArgs e)
    {
        var userRegistrationWindow = new UserRegistrationWindow(_dataService);
        userRegistrationWindow.Owner = this;
        userRegistrationWindow.ShowDialog();
    }

    /// <summary>
    /// Initializes the window asynchronously.
    /// </summary>
    private async void InitializeAsync()
    {
        if (_dataService.IsUserAuthenticated())
        {
            await LoadBoardsAsync();
        }
        else
        {
            MessageBox.Show("Authentication error. Please log in again.");
            Application.Current.Shutdown();
        }
    }

    /// <summary>
    /// Loads the boards for the current user.
    /// </summary>
    private async Task LoadBoardsAsync()
    {
        boards = await _dataService.GetBoardsAsync();
        ProjectListBox.ItemsSource = boards;
        if (boards.Any()) ProjectListBox.SelectedItem = boards.First();
    }

    /// <summary>
    /// Handles the click event for creating a new board.
    /// </summary>
    private async void NewBoard_Click(object sender, RoutedEventArgs e)
    {
        var inputDialog = new InputDialog(this, "New Board", "Enter the name of the new board:");
        if (inputDialog.ShowDialog() == true)
        {
            var boardName = inputDialog.Input;
            if (!string.IsNullOrWhiteSpace(boardName))
            {
                var newBoard = await _dataService.AddBoardAsync(boardName);
                boards.Add(newBoard);
                ProjectListBox.Items.Refresh();
                ProjectListBox.SelectedItem = newBoard;
                currentBoard = newBoard;
                ListsItemsControl.ItemsSource = currentBoard.Lists;
            }
        }
    }

    /// <summary>
    /// Handles the selection change event for the project list.
    /// </summary>
    private async void ProjectListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ProjectListBox.SelectedItem is Board selectedBoard)
        {
            currentBoard = await _dataService.GetBoardAsync(selectedBoard.Id);
            if (currentBoard != null)
                ListsItemsControl.ItemsSource = currentBoard.Lists;
            else
                MessageBox.Show("The selected board could not be loaded.");
        }
    }

    /// <summary>
    /// Handles the click event for adding a new list.
    /// </summary>
    private async void AddList_Click(object sender, RoutedEventArgs e)
    {
        if (currentBoard != null)
        {
            var inputDialog = new InputDialog(this, "New List", "Enter the name of the new list:");
            if (inputDialog.ShowDialog() == true)
            {
                var listName = inputDialog.Input;
                if (!string.IsNullOrWhiteSpace(listName))
                    try
                    {
                        await _dataService.AddTaskListAsync(currentBoard.Id, listName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error adding the list: {ex.Message}");
                    }
            }
        }
        else
        {
            MessageBox.Show("Please select a board first.");
        }
    }

    /// <summary>
    /// Refreshes the current board.
    /// </summary>
    private async Task RefreshCurrentBoard()
    {
        if (currentBoard != null)
        {
            currentBoard = await _dataService.GetBoardAsync(currentBoard.Id);
            ListsItemsControl.ItemsSource = currentBoard?.Lists;
        }
    }

    /// <summary>
    /// Handles the click event for editing a project.
    /// </summary>
    private async void EditProject_Click(object sender, RoutedEventArgs e)
    {
        if (sender is FrameworkElement element && element.DataContext is Board board)
        {
            var inputDialog = new InputDialog(this, "Rename Project", "Enter the new name for the project:", board.Name);
            if (inputDialog.ShowDialog() == true)
            {
                var newName = inputDialog.Input;
                if (!string.IsNullOrWhiteSpace(newName))
                {
                    board.Name = newName;
                    await _dataService.UpdateBoardAsync(board);
                    ProjectListBox.Items.Refresh();
                }
            }
        }
    }

    /// <summary>
    /// Handles the click event for editing a list.
    /// </summary>
    private async void EditList_Click(object sender, RoutedEventArgs e)
    {
        if (sender is FrameworkElement element && element.DataContext is TaskList taskList)
        {
            var inputDialog = new InputDialog(this, "Rename List", "Enter the new name for the list:", taskList.Name);
            if (inputDialog.ShowDialog() == true)
            {
                var newName = inputDialog.Input;
                if (!string.IsNullOrWhiteSpace(newName))
                {
                    taskList.Name = newName;
                    await _dataService.UpdateTaskListAsync(taskList);
                    ListsItemsControl.Items.Refresh();
                }
            }
        }
    }

    /// <summary>
    /// Handles the click event for adding a new card.
    /// </summary>
    private async void AddCard_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is TaskList taskList)
        {
            var inputDialog = new InputDialog(this, "New Card", "Enter the name of the new card:");
            if (inputDialog.ShowDialog() == true)
            {
                var cardName = inputDialog.Input;
                if (!string.IsNullOrWhiteSpace(cardName))
                    try
                    {
                        await _dataService.AddTaskAsync(taskList.Id, cardName);
                        ListsItemsControl.Items.Refresh();
                    }
                    catch (Exception ex)
                    {
                        var innerException = ex.InnerException;
                        var errorMessage = $"Error adding the card: {ex.Message}";
                        while (innerException != null)
                        {
                            errorMessage += $"\n\nInner Exception: {innerException.Message}";
                            innerException = innerException.InnerException;
                        }
                        MessageBox.Show(errorMessage);
                    }
            }
        }
    }

    /// <summary>
    /// Handles the click event for editing a task.
    /// </summary>
    private async void EditTask_Click(object sender, RoutedEventArgs e)
    {
        if (sender is FrameworkElement element && element.DataContext is HeroTask task)
        {
            var inputDialog = new InputDialog(this, "Rename Task", "Enter the new name for the task:", task.Name);
            if (inputDialog.ShowDialog() == true)
            {
                var newName = inputDialog.Input;
                if (!string.IsNullOrWhiteSpace(newName))
                {
                    task.Name = newName;
                    await _dataService.UpdateTaskAsync(task);
                    ListsItemsControl.Items.Refresh();
                }
            }
        }
    }

    /// <summary>
    /// Handles the mouse left button down event for a card.
    /// </summary>
    private async void Card_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (sender is FrameworkElement element && element.DataContext is HeroTask task)
        {
            var cardDetailWindow = new CardDetailWindow(task);
            cardDetailWindow.Owner = this;
            if (cardDetailWindow.ShowDialog() == true)
            {
                await _dataService.UpdateTaskAsync(task);
                ListsItemsControl.Items.Refresh();
            }
        }
    }

    /// <summary>
    /// Handles the click event for deleting a project.
    /// </summary>
    private async void DeleteProject_Click(object sender, RoutedEventArgs e)
    {
        if (sender is FrameworkElement element && element.DataContext is Board board)
        {
            var result = MessageBox.Show($"Are you sure you want to delete the board '{board.Name}'?", "Delete Board", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                await _dataService.DeleteBoardAsync(board.Id);
                boards.Remove(board);
                ProjectListBox.Items.Refresh();
                if (currentBoard == board)
                {
                    currentBoard = null;
                    ListsItemsControl.ItemsSource = null;
                }
            }
        }
    }

    /// <summary>
    /// Handles the click event for deleting a list.
    /// </summary>
    private async void DeleteList_Click(object sender, RoutedEventArgs e)
    {
        if (sender is FrameworkElement element && element.DataContext is TaskList taskList)
        {
            var result = MessageBox.Show($"Are you sure you want to delete the list '{taskList.Name}'?", "Delete List", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                await _dataService.DeleteTaskListAsync(taskList.Id);
                await RefreshCurrentBoard();
            }
        }
    }

    /// <summary>
    /// Handles the click event for deleting a task.
    /// </summary>
    private async void DeleteTask_Click(object sender, RoutedEventArgs e)
    {
        if (sender is FrameworkElement element && element.DataContext is HeroTask task)
        {
            var result = MessageBox.Show($"Are you sure you want to delete the task '{task.Name}'?", "Delete Task", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                await _dataService.DeleteTaskAsync(task.Id);
                await RefreshCurrentBoard();
            }
        }
    }
}