using TaskFlask.Models;
using TaskStatus = TaskFlask.Models.TaskStatus;

namespace TaskFlask.Utils
{
    /// <summary>
    /// Helper class for filtering and sorting tasks
    /// </summary>
    public static class TaskFilterHelper
    {
        /// <summary>
        /// Filters tasks based on various criteria
        /// </summary>
        /// <param name="tasks">Collection of tasks to filter</param>
        /// <param name="filterType">Type of filter to apply</param>
        /// <param name="showCompleted">Whether to include completed tasks</param>
        /// <param name="searchText">Optional search text to filter by</param>
        /// <param name="selectedCategory">Optional category to filter by</param>
        /// <returns>Filtered collection of tasks</returns>
        public static IEnumerable<TaskItem> FilterTasks(
            IEnumerable<TaskItem> tasks,
            string filterType = "All",
            bool showCompleted = true,
            string? searchText = null,
            string? selectedCategory = null)
        {
            var filteredTasks = tasks.AsEnumerable();

            // Apply status filter
            switch (filterType)
            {
                case "Not Started":
                    filteredTasks = filteredTasks.Where(t => t.Status == TaskStatus.NotStarted);
                    break;
                case "In Progress":
                    filteredTasks = filteredTasks.Where(t => t.Status == TaskStatus.InProgress);
                    break;
                case "Completed":
                    filteredTasks = filteredTasks.Where(t => t.Status == TaskStatus.Completed);
                    break;
                case "Overdue":
                    filteredTasks = filteredTasks.Where(t => t.IsOverdue);
                    break;
                case "High Priority":
                    filteredTasks = filteredTasks.Where(t => t.Priority == TaskPriority.High || t.Priority == TaskPriority.Critical);
                    break;
                case "Due Today":
                    filteredTasks = filteredTasks.Where(t => t.DueDate?.Date == DateTime.Today);
                    break;
                case "Due This Week":
                    var startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
                    var endOfWeek = startOfWeek.AddDays(7);
                    filteredTasks = filteredTasks.Where(t => t.DueDate?.Date >= startOfWeek && t.DueDate?.Date < endOfWeek);
                    break;
            }

            // Apply completed filter
            if (!showCompleted)
            {
                filteredTasks = filteredTasks.Where(t => t.Status != TaskStatus.Completed);
            }

            // Apply search text filter
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                var search = searchText.ToLowerInvariant();
                filteredTasks = filteredTasks.Where(t =>
                    t.Title.ToLowerInvariant().Contains(search) ||
                    t.Description.ToLowerInvariant().Contains(search) ||
                    t.Categories.Any(c => c.ToLowerInvariant().Contains(search)));
            }

            // Apply category filter
            if (!string.IsNullOrWhiteSpace(selectedCategory))
            {
                filteredTasks = filteredTasks.Where(t =>
                    t.Categories.Contains(selectedCategory, StringComparer.OrdinalIgnoreCase));
            }

            return filteredTasks;
        }

        /// <summary>
        /// Sorts tasks based on specified criteria
        /// </summary>
        /// <param name="tasks">Collection of tasks to sort</param>
        /// <param name="sortBy">Field to sort by</param>
        /// <param name="descending">Whether to sort in descending order</param>
        /// <returns>Sorted collection of tasks</returns>
        public static IEnumerable<TaskItem> SortTasks(
            IEnumerable<TaskItem> tasks,
            TaskSortBy sortBy,
            bool descending = false)
        {
            return sortBy switch
            {
                TaskSortBy.Title => descending
                    ? tasks.OrderByDescending(t => t.Title, StringComparer.OrdinalIgnoreCase)
                    : tasks.OrderBy(t => t.Title, StringComparer.OrdinalIgnoreCase),
                
                TaskSortBy.DueDate => descending
                    ? tasks.OrderByDescending(t => t.DueDate ?? DateTime.MaxValue)
                    : tasks.OrderBy(t => t.DueDate ?? DateTime.MaxValue),
                
                TaskSortBy.Priority => descending
                    ? tasks.OrderByDescending(t => (int)t.Priority)
                    : tasks.OrderBy(t => (int)t.Priority),
                
                TaskSortBy.Status => descending
                    ? tasks.OrderByDescending(t => (int)t.Status)
                    : tasks.OrderBy(t => (int)t.Status),
                
                TaskSortBy.ModifiedDate => descending
                    ? tasks.OrderByDescending(t => t.ModifiedDate)
                    : tasks.OrderBy(t => t.ModifiedDate),
                
                TaskSortBy.CreatedDate => descending
                    ? tasks.OrderByDescending(t => t.CreatedDate)
                    : tasks.OrderBy(t => t.CreatedDate),
                
                _ => descending
                    ? tasks.OrderByDescending(t => t.CreatedDate)
                    : tasks.OrderBy(t => t.CreatedDate)
            };
        }

        /// <summary>
        /// Gets task statistics for a collection of tasks
        /// </summary>
        /// <param name="tasks">Collection of tasks to analyze</param>
        /// <returns>Task statistics</returns>
        public static TaskStatistics GetTaskStatistics(IEnumerable<TaskItem> tasks)
        {
            var taskList = tasks.ToList();
            
            return new TaskStatistics
            {
                TotalTasks = taskList.Count,
                CompletedTasks = taskList.Count(t => t.Status == TaskStatus.Completed),
                InProgressTasks = taskList.Count(t => t.Status == TaskStatus.InProgress),
                NotStartedTasks = taskList.Count(t => t.Status == TaskStatus.NotStarted),
                OverdueTasks = taskList.Count(t => t.IsOverdue),
                HighPriorityTasks = taskList.Count(t => t.Priority == TaskPriority.High || t.Priority == TaskPriority.Critical),
                DueTodayTasks = taskList.Count(t => t.DueDate?.Date == DateTime.Today),
                DueThisWeekTasks = taskList.Count(t => t.DueDate?.Date >= DateTime.Today && t.DueDate?.Date <= DateTime.Today.AddDays(7))
            };
        }
    }

    /// <summary>
    /// Represents task statistics
    /// </summary>
    public class TaskStatistics
    {
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int InProgressTasks { get; set; }
        public int NotStartedTasks { get; set; }
        public int OverdueTasks { get; set; }
        public int HighPriorityTasks { get; set; }
        public int DueTodayTasks { get; set; }
        public int DueThisWeekTasks { get; set; }

        public double CompletionPercentage => TotalTasks > 0 ? (double)CompletedTasks / TotalTasks * 100 : 0;
        public int PendingTasks => TotalTasks - CompletedTasks;
    }
}
