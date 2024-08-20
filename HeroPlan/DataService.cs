using Microsoft.EntityFrameworkCore;

namespace HeroPlan;

public class DataService
{
    private readonly HeroPlanDbContext _context;
    private User _currentUser;

    public DataService()
    {
        _context = new HeroPlanDbContext();
    }

    /// <summary>
    /// Authenticates a user with the given username and password.
    /// </summary>
    public async Task<bool> AuthenticateUserAsync(string username, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            _currentUser = user;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Checks if a user is currently authenticated.
    /// </summary>
    public bool IsUserAuthenticated()
    {
        return _currentUser != null;
    }

    /// <summary>
    /// Retrieves a board by its ID, including its lists and tasks.
    /// </summary>
    public async Task<Board?> GetBoardAsync(int boardId)
    {
        return await _context.Boards
            .Include(b => b.Lists)
            .ThenInclude(l => l.Tasks)
            .FirstOrDefaultAsync(b => b.Id == boardId);
    }

    /// <summary>
    /// Retrieves all boards for the current user, including their lists and tasks.
    /// </summary>
    public async Task<List<Board>> GetBoardsAsync()
    {
        return _currentUser == null
            ? throw new InvalidOperationException("Der Benutzer ist nicht authentifiziert.")
            : await _context.Boards
            .Where(b => b.UserId == _currentUser.Id)
            .Include(b => b.Lists)
            .ThenInclude(l => l.Tasks)
            .ToListAsync();
    }

    /// <summary>
    /// Adds a new board for the current user.
    /// </summary>
    public async Task<Board> AddBoardAsync(string name)
    {
        var board = new Board { Name = name, UserId = _currentUser.Id };
        _context.Boards.Add(board);
        await _context.SaveChangesAsync();
        return board;
    }

    /// <summary>
    /// Updates an existing board.
    /// </summary>
    public async Task<int> UpdateBoardAsync(Board? board)
    {
        _context.Boards.Update(board);
        return await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Adds a new task list to a specific board.
    /// </summary>
    public async Task<TaskList> AddTaskListAsync(int boardId, string name)
    {
        var board = await _context.Boards.FindAsync(boardId);
        if (board == null) throw new ArgumentException("Board not found", nameof(boardId));
        var taskList = new TaskList { Name = name, BoardId = boardId };
        _context.TaskLists.Add(taskList);
        await _context.SaveChangesAsync();
        return taskList;
    }

    /// <summary>
    /// Updates an existing task list.
    /// </summary>
    public async Task<int> UpdateTaskListAsync(TaskList taskList)
    {
        _context.TaskLists.Update(taskList);
        return await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Adds a new task to a specific task list.
    /// </summary>
    public async Task<HeroTask> AddTaskAsync(int taskListId, string name)
    {
        var taskList = await _context.TaskLists
            .Include(tl => tl.Tasks)
            .FirstOrDefaultAsync(tl => tl.Id == taskListId);
        if (taskList == null) throw new ArgumentException("TaskList not found", nameof(taskListId));
        var task = new HeroTask
        {
            Name = name,
            Description = string.Empty // Avoid NULL values
        };
        taskList.Tasks.Add(task);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to add task to database", ex);
        }
        return task;
    }

    /// <summary>
    /// Updates an existing task.
    /// </summary>
    public async Task<int> UpdateTaskAsync(HeroTask task)
    {
        _context.Tasks.Update(task);
        return await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Adds a new user to the system.
    /// </summary>
    public async Task AddUserAsync(string username, string password)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (existingUser != null)
        {
            throw new InvalidOperationException("Ein Benutzer mit diesem Namen existiert bereits.");
        }

        var newUser = new User
        {
            Username = username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Checks if the current user is an admin.
    /// </summary>
    public bool IsAdminUser()
    {
        return _currentUser?.Username == "admin";
    }

    /// <summary>
    /// Deletes a board by its ID.
    /// </summary>
    public async Task DeleteBoardAsync(int boardId)
    {
        var board = await _context.Boards.FindAsync(boardId);
        if (board != null)
        {
            _context.Boards.Remove(board);
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Deletes a task list by its ID.
    /// </summary>
    public async Task DeleteTaskListAsync(int taskListId)
    {
        var taskList = await _context.TaskLists.FindAsync(taskListId);
        if (taskList != null)
        {
            _context.TaskLists.Remove(taskList);
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Deletes a task by its ID.
    /// </summary>
    public async Task DeleteTaskAsync(int taskId)
    {
        var task = await _context.Tasks.FindAsync(taskId);
        if (task != null)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}