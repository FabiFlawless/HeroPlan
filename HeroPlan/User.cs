namespace HeroPlan;

/// <summary>
/// Represents a user in the HeroPlan system.
/// </summary>
public class User
{
    /// <summary>
    /// Gets or sets the unique identifier for the user.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the hashed password of the user.
    /// This is stored as a hash for security reasons.
    /// </summary>
    public string PasswordHash { get; set; }

    /// <summary>
    /// Gets or sets the list of boards owned by the user.
    /// Initialized as an empty list.
    /// </summary>
    public List<Board> Boards { get; set; } = new List<Board>();
}