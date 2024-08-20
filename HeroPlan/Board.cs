namespace HeroPlan;
/// <summary>
///     Represents a board (project) in the HeroPlan system.
/// </summary>
public class Board
{
    /// <summary>
    ///     The unique identifier for the board.
    /// </summary>
    public int Id {get;set;}

    /// <summary>
    ///     The name of the board.
    /// </summary>
    public string Name {get;set;}

    /// <summary>
    ///     The collection of TaskLists associated with this board.
    ///     Uses ObservableCollection for UI notifications.
    /// </summary>
    public ObservableCollection<TaskList> Lists {get;set;} = new();

    /// <summary>
    ///     The ID of the user who owns this board.
    /// </summary>
    public int UserId {get;set;}

    /// <summary>
    ///     The user who owns this board.
    /// </summary>
    public User User {get;set;}

    /// <summary>
    ///     Returns a string representation of the board.
    /// </summary>
    /// <returns>The name of the board.</returns>
    public override string ToString()
    {
        return Name;
    }
}