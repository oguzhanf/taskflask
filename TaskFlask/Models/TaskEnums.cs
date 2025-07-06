namespace TaskFlask.Models
{
    /// <summary>
    /// Represents the priority level of a task
    /// </summary>
    public enum TaskPriority
    {
        Low = 1,
        Medium = 2,
        High = 3,
        Critical = 4
    }

    /// <summary>
    /// Represents the current status of a task
    /// </summary>
    public enum TaskStatus
    {
        NotStarted = 0,
        InProgress = 1,
        Completed = 2,
        Cancelled = 3,
        OnHold = 4
    }

    /// <summary>
    /// Represents different sorting options for tasks
    /// </summary>
    public enum TaskSortBy
    {
        Title,
        DueDate,
        Priority,
        Status,
        CreatedDate,
        ModifiedDate
    }

    /// <summary>
    /// Represents sorting direction
    /// </summary>
    public enum SortDirection
    {
        Ascending,
        Descending
    }
}
