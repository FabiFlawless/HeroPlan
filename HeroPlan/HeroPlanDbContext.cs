namespace HeroPlan;
/// <summary>
///     Represents the database context for the HeroPlan application.
/// </summary>
public class HeroPlanDbContext : DbContext
{
    /// <summary>
    ///     Gets or sets the Users table.
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    ///     Gets or sets the Boards table.
    /// </summary>
    public DbSet<Board> Boards { get; set; }

    /// <summary>
    ///     Gets or sets the TaskLists table.
    /// </summary>
    public DbSet<TaskList> TaskLists { get; set; }

    /// <summary>
    ///     Gets or sets the Tasks table.
    /// </summary>
    public DbSet<HeroTask> Tasks { get; set; }

    /// <summary>
    ///     Configures the database connection.
    /// </summary>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            @"Server=(localdb)\mssqllocaldb;Database=HeroPlanDb;Trusted_Connection=True;");
    }

    /// <summary>
    ///     Configures the database model.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure one-to-many relationship between User and Board
        modelBuilder.Entity<User>()
            .HasMany(u => u.Boards)
            .WithOne(b => b.User)
            .HasForeignKey(b => b.UserId);

        // Configure one-to-many relationship between Board and TaskList
        modelBuilder.Entity<Board>()
            .HasMany(b => b.Lists)
            .WithOne(l => l.Board)
            .HasForeignKey(l => l.BoardId);

        // Configure one-to-many relationship between TaskList and HeroTask
        modelBuilder.Entity<TaskList>()
            .HasMany(l => l.Tasks)
            .WithOne(t => t.TaskList)
            .HasForeignKey(t => t.TaskListId);

        // Seed admin user
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin")
            }
        );
    }
}