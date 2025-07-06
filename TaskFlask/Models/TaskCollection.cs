using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Serialization;

namespace TaskFlask.Models
{
    /// <summary>
    /// Represents a collection of tasks that can be serialized to XML
    /// </summary>
    [Serializable]
    [XmlRoot("TaskFlaskData")]
    public class TaskCollection : INotifyPropertyChanged
    {
        private ObservableCollection<TaskItem> _tasks = new();
        private string _version = "1.0";
        private DateTime _lastModified = DateTime.Now;

        public event PropertyChangedEventHandler? PropertyChanged;

        [XmlAttribute]
        public string Version
        {
            get => _version;
            set
            {
                if (_version != value)
                {
                    _version = value;
                    OnPropertyChanged(nameof(Version));
                }
            }
        }

        public DateTime LastModified
        {
            get => _lastModified;
            set
            {
                if (_lastModified != value)
                {
                    _lastModified = value;
                    OnPropertyChanged(nameof(LastModified));
                }
            }
        }

        [XmlArray("Tasks")]
        [XmlArrayItem("Task")]
        public ObservableCollection<TaskItem> Tasks
        {
            get => _tasks;
            set
            {
                if (_tasks != value)
                {
                    if (_tasks != null)
                    {
                        _tasks.CollectionChanged -= Tasks_CollectionChanged;
                    }

                    _tasks = value ?? new ObservableCollection<TaskItem>();
                    _tasks.CollectionChanged += Tasks_CollectionChanged;
                    OnPropertyChanged(nameof(Tasks));
                    UpdateLastModified();
                }
            }
        }

        [XmlIgnore]
        public int TotalTasks => Tasks.Count;

        [XmlIgnore]
        public int CompletedTasks => Tasks.Count(t => t.Status == TaskStatus.Completed);

        [XmlIgnore]
        public int PendingTasks => Tasks.Count(t => t.Status != TaskStatus.Completed && t.Status != TaskStatus.Cancelled);

        [XmlIgnore]
        public int OverdueTasks => Tasks.Count(t => t.IsOverdue);

        public TaskCollection()
        {
            Tasks = new ObservableCollection<TaskItem>();
        }

        private void Tasks_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateLastModified();
            OnPropertyChanged(nameof(TotalTasks));
            OnPropertyChanged(nameof(CompletedTasks));
            OnPropertyChanged(nameof(PendingTasks));
            OnPropertyChanged(nameof(OverdueTasks));
        }

        public void AddTask(TaskItem task)
        {
            if (task != null)
            {
                Tasks.Add(task);
            }
        }

        public void RemoveTask(TaskItem task)
        {
            if (task != null)
            {
                Tasks.Remove(task);
            }
        }

        public void RemoveTask(string taskId)
        {
            var task = Tasks.FirstOrDefault(t => t.Id == taskId);
            if (task != null)
            {
                Tasks.Remove(task);
            }
        }

        public TaskItem? GetTask(string taskId)
        {
            return Tasks.FirstOrDefault(t => t.Id == taskId);
        }

        public List<TaskItem> GetTasksByStatus(TaskStatus status)
        {
            return Tasks.Where(t => t.Status == status).ToList();
        }

        public List<TaskItem> GetTasksByPriority(TaskPriority priority)
        {
            return Tasks.Where(t => t.Priority == priority).ToList();
        }

        public List<TaskItem> GetTasksByCategory(string category)
        {
            return Tasks.Where(t => t.Categories.Contains(category, StringComparer.OrdinalIgnoreCase)).ToList();
        }

        public List<string> GetAllCategories()
        {
            return Tasks.SelectMany(t => t.Categories)
                       .Distinct(StringComparer.OrdinalIgnoreCase)
                       .OrderBy(c => c)
                       .ToList();
        }

        private void UpdateLastModified()
        {
            LastModified = DateTime.Now;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
