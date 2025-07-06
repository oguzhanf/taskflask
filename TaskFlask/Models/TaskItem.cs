using System.ComponentModel;
using System.Xml.Serialization;

namespace TaskFlask.Models
{
    /// <summary>
    /// Represents a task item with all its properties
    /// </summary>
    [Serializable]
    public class TaskItem : INotifyPropertyChanged
    {
        private string _id = string.Empty;
        private string _title = string.Empty;
        private string _description = string.Empty;
        private DateTime? _dueDate;
        private TaskPriority _priority = TaskPriority.Medium;
        private TaskStatus _status = TaskStatus.NotStarted;
        private List<string> _categories = new();
        private DateTime _createdDate = DateTime.Now;
        private DateTime _modifiedDate = DateTime.Now;

        public event PropertyChangedEventHandler? PropertyChanged;

        [XmlAttribute]
        public string Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    ModifiedDate = DateTime.Now;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    ModifiedDate = DateTime.Now;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        public DateTime? DueDate
        {
            get => _dueDate;
            set
            {
                if (_dueDate != value)
                {
                    _dueDate = value;
                    ModifiedDate = DateTime.Now;
                    OnPropertyChanged(nameof(DueDate));
                }
            }
        }

        public TaskPriority Priority
        {
            get => _priority;
            set
            {
                if (_priority != value)
                {
                    _priority = value;
                    ModifiedDate = DateTime.Now;
                    OnPropertyChanged(nameof(Priority));
                }
            }
        }

        public TaskStatus Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    ModifiedDate = DateTime.Now;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }

        public List<string> Categories
        {
            get => _categories;
            set
            {
                _categories = value ?? new List<string>();
                ModifiedDate = DateTime.Now;
                OnPropertyChanged(nameof(Categories));
            }
        }

        public DateTime CreatedDate
        {
            get => _createdDate;
            set
            {
                if (_createdDate != value)
                {
                    _createdDate = value;
                    OnPropertyChanged(nameof(CreatedDate));
                }
            }
        }

        public DateTime ModifiedDate
        {
            get => _modifiedDate;
            set
            {
                if (_modifiedDate != value)
                {
                    _modifiedDate = value;
                    OnPropertyChanged(nameof(ModifiedDate));
                }
            }
        }

        [XmlIgnore]
        public bool IsOverdue => DueDate.HasValue && DueDate.Value.Date < DateTime.Today && Status != TaskStatus.Completed;

        [XmlIgnore]
        public string CategoriesString => string.Join(", ", Categories);

        public TaskItem()
        {
            Id = Guid.NewGuid().ToString();
        }

        public TaskItem(string title) : this()
        {
            Title = title;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
