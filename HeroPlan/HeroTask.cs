namespace HeroPlan;
/// <summary>
///     Represents a task in the HeroPlan system.
/// </summary>
public class HeroTask
{
    /// <summary>
    ///     Gets or sets the name of the task.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Gets or sets the description of the task.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     Gets or sets the deadline of the task. Can be null if no deadline is set.
    /// </summary>
    public DateTime? Deadline { get; set; }

    /// <summary>
    ///     Gets or sets the unique identifier for the task.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Gets or sets the TaskList this task belongs to.
    /// </summary>
    public TaskList TaskList { get; set; }

    /// <summary>
    ///     Gets or sets the ID of the TaskList this task belongs to.
    /// </summary>
    public int TaskListId { get; set; }

    /// <summary>
    ///     Indicates whether the task is overdue.
    ///     A task is considered overdue if it has a deadline and the deadline is before today.
    /// </summary>
    public bool IsOverdue => Deadline.HasValue && Deadline.Value.Date < DateTime.Today;
}