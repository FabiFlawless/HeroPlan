namespace HeroPlan;
/// <summary>
///     Represents a task list in the HeroPlan system.
/// </summary>
public class TaskList
{
    /// <summary>
    ///     Gets or sets the unique identifier for the task list.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Gets or sets the name of the task list.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Gets or sets the collection of tasks associated with this task list.
    ///     Uses ObservableCollection for UI notifications.
    /// </summary>
    public ObservableCollection<HeroTask> Tasks { get; set; } = new();

    /// <summary>
    ///     Gets or sets the ID of the board this task list belongs to.
    /// </summary>
    public int BoardId { get; set; }

    /// <summary>
    ///     Gets or sets the board this task list belongs to.
    /// </summary>
    public Board Board { get; set; }
}