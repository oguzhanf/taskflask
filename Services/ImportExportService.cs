using System.Text;
using System.Text.Json;
using TaskFlask.Models;

namespace TaskFlask.Services
{
    /// <summary>
    /// Service for importing and exporting task data in various formats
    /// </summary>
    public class ImportExportService
    {
        /// <summary>
        /// Exports tasks to CSV format
        /// </summary>
        /// <param name="tasks">Tasks to export</param>
        /// <param name="filePath">Path to save the CSV file</param>
        public async Task ExportToCsvAsync(IEnumerable<TaskItem> tasks, string filePath)
        {
            var csv = new StringBuilder();
            
            // Add header
            csv.AppendLine("Title,Description,Priority,Status,DueDate,Categories,CreatedDate,ModifiedDate");
            
            // Add tasks
            foreach (var task in tasks)
            {
                var line = $"\"{EscapeCsv(task.Title)}\"," +
                          $"\"{EscapeCsv(task.Description)}\"," +
                          $"\"{task.Priority}\"," +
                          $"\"{task.Status}\"," +
                          $"\"{task.DueDate?.ToString("yyyy-MM-dd") ?? ""}\"," +
                          $"\"{EscapeCsv(string.Join("; ", task.Categories))}\"," +
                          $"\"{task.CreatedDate:yyyy-MM-dd HH:mm:ss}\"," +
                          $"\"{task.ModifiedDate:yyyy-MM-dd HH:mm:ss}\"";
                
                csv.AppendLine(line);
            }
            
            await File.WriteAllTextAsync(filePath, csv.ToString(), Encoding.UTF8);
        }

        /// <summary>
        /// Exports tasks to JSON format
        /// </summary>
        /// <param name="tasks">Tasks to export</param>
        /// <param name="filePath">Path to save the JSON file</param>
        public async Task ExportToJsonAsync(IEnumerable<TaskItem> tasks, string filePath)
        {
            var exportData = new
            {
                ExportDate = DateTime.Now,
                Version = "1.0",
                Tasks = tasks.Select(t => new
                {
                    t.Id,
                    t.Title,
                    t.Description,
                    Priority = t.Priority.ToString(),
                    Status = t.Status.ToString(),
                    DueDate = t.DueDate?.ToString("yyyy-MM-dd"),
                    t.Categories,
                    CreatedDate = t.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    ModifiedDate = t.ModifiedDate.ToString("yyyy-MM-dd HH:mm:ss")
                })
            };

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(exportData, options);
            await File.WriteAllTextAsync(filePath, json, Encoding.UTF8);
        }

        /// <summary>
        /// Imports tasks from CSV format
        /// </summary>
        /// <param name="filePath">Path to the CSV file</param>
        /// <returns>List of imported tasks</returns>
        public async Task<List<TaskItem>> ImportFromCsvAsync(string filePath)
        {
            var tasks = new List<TaskItem>();
            var lines = await File.ReadAllLinesAsync(filePath, Encoding.UTF8);
            
            if (lines.Length <= 1) return tasks; // No data or header only
            
            // Skip header line
            for (int i = 1; i < lines.Length; i++)
            {
                try
                {
                    var task = ParseCsvLine(lines[i]);
                    if (task != null)
                    {
                        tasks.Add(task);
                    }
                }
                catch
                {
                    // Skip invalid lines
                }
            }
            
            return tasks;
        }

        /// <summary>
        /// Imports tasks from JSON format
        /// </summary>
        /// <param name="filePath">Path to the JSON file</param>
        /// <returns>List of imported tasks</returns>
        public async Task<List<TaskItem>> ImportFromJsonAsync(string filePath)
        {
            var tasks = new List<TaskItem>();
            var json = await File.ReadAllTextAsync(filePath, Encoding.UTF8);
            
            try
            {
                using var document = JsonDocument.Parse(json);
                var root = document.RootElement;
                
                if (root.TryGetProperty("tasks", out var tasksElement) ||
                    root.TryGetProperty("Tasks", out tasksElement))
                {
                    foreach (var taskElement in tasksElement.EnumerateArray())
                    {
                        try
                        {
                            var task = ParseJsonTask(taskElement);
                            if (task != null)
                            {
                                tasks.Add(task);
                            }
                        }
                        catch
                        {
                            // Skip invalid tasks
                        }
                    }
                }
            }
            catch
            {
                // Invalid JSON format
            }
            
            return tasks;
        }

        /// <summary>
        /// Parses a CSV line into a TaskItem
        /// </summary>
        private TaskItem? ParseCsvLine(string line)
        {
            var fields = ParseCsvFields(line);
            if (fields.Length < 8) return null;
            
            var task = new TaskItem
            {
                Title = fields[0],
                Description = fields[1]
            };
            
            // Parse priority
            if (Enum.TryParse<TaskPriority>(fields[2], out var priority))
            {
                task.Priority = priority;
            }
            
            // Parse status
            if (Enum.TryParse<TaskStatus>(fields[3], out var status))
            {
                task.Status = status;
            }
            
            // Parse due date
            if (DateTime.TryParse(fields[4], out var dueDate))
            {
                task.DueDate = dueDate;
            }
            
            // Parse categories
            if (!string.IsNullOrEmpty(fields[5]))
            {
                task.Categories = fields[5].Split(';')
                    .Select(c => c.Trim())
                    .Where(c => !string.IsNullOrEmpty(c))
                    .ToList();
            }
            
            // Parse created date
            if (DateTime.TryParse(fields[6], out var createdDate))
            {
                task.CreatedDate = createdDate;
            }
            
            // Parse modified date
            if (DateTime.TryParse(fields[7], out var modifiedDate))
            {
                task.ModifiedDate = modifiedDate;
            }
            
            return task;
        }

        /// <summary>
        /// Parses a JSON task element into a TaskItem
        /// </summary>
        private TaskItem? ParseJsonTask(JsonElement taskElement)
        {
            var task = new TaskItem();
            
            if (taskElement.TryGetProperty("title", out var titleElement))
                task.Title = titleElement.GetString() ?? "";
            
            if (taskElement.TryGetProperty("description", out var descElement))
                task.Description = descElement.GetString() ?? "";
            
            if (taskElement.TryGetProperty("priority", out var priorityElement) &&
                Enum.TryParse<TaskPriority>(priorityElement.GetString(), out var priority))
                task.Priority = priority;
            
            if (taskElement.TryGetProperty("status", out var statusElement) &&
                Enum.TryParse<TaskStatus>(statusElement.GetString(), out var status))
                task.Status = status;
            
            if (taskElement.TryGetProperty("dueDate", out var dueDateElement) &&
                DateTime.TryParse(dueDateElement.GetString(), out var dueDate))
                task.DueDate = dueDate;
            
            if (taskElement.TryGetProperty("categories", out var categoriesElement))
            {
                task.Categories = categoriesElement.EnumerateArray()
                    .Select(c => c.GetString() ?? "")
                    .Where(c => !string.IsNullOrEmpty(c))
                    .ToList();
            }
            
            if (taskElement.TryGetProperty("createdDate", out var createdElement) &&
                DateTime.TryParse(createdElement.GetString(), out var created))
                task.CreatedDate = created;
            
            if (taskElement.TryGetProperty("modifiedDate", out var modifiedElement) &&
                DateTime.TryParse(modifiedElement.GetString(), out var modified))
                task.ModifiedDate = modified;
            
            return string.IsNullOrEmpty(task.Title) ? null : task;
        }

        /// <summary>
        /// Parses CSV fields handling quoted values
        /// </summary>
        private string[] ParseCsvFields(string line)
        {
            var fields = new List<string>();
            var current = new StringBuilder();
            bool inQuotes = false;
            
            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                
                if (c == '"')
                {
                    inQuotes = !inQuotes;
                }
                else if (c == ',' && !inQuotes)
                {
                    fields.Add(current.ToString());
                    current.Clear();
                }
                else
                {
                    current.Append(c);
                }
            }
            
            fields.Add(current.ToString());
            return fields.ToArray();
        }

        /// <summary>
        /// Escapes CSV values
        /// </summary>
        private string EscapeCsv(string value)
        {
            if (string.IsNullOrEmpty(value)) return "";
            return value.Replace("\"", "\"\"");
        }
    }
}
