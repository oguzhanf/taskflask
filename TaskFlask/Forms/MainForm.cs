using System.ComponentModel;
using TaskFlask.Models;
using TaskFlask.Services;
using TaskStatus = TaskFlask.Models.TaskStatus;

namespace TaskFlask.Forms
{
    /// <summary>
    /// Main application window for TaskFlask
    /// </summary>
    public partial class MainForm : Form
    {
        private readonly SettingsService _settingsService;
        private readonly XmlDataService _dataService;
        private TaskCollection _taskCollection;
        private BindingList<TaskItem> _filteredTasks;
        private System.Windows.Forms.Timer? _autoSaveTimer;

        public MainForm(SettingsService settingsService)
        {
            InitializeComponent();
            
            _settingsService = settingsService ?? throw new ArgumentNullException(nameof(settingsService));
            _dataService = new XmlDataService(_settingsService.Settings.DatabaseFilePath);
            _taskCollection = new TaskCollection();
            _filteredTasks = new BindingList<TaskItem>();

            InitializeUI();
            SetupAutoSave();
            
            this.Load += MainForm_Load;
            this.FormClosing += MainForm_FormClosing;
        }

        private void InitializeUI()
        {
            // Set window properties from settings
            var settings = _settingsService.Settings;
            
            if (settings.WindowX >= 0 && settings.WindowY >= 0)
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(settings.WindowX, settings.WindowY);
            }
            
            this.Size = new Size(settings.WindowWidth, settings.WindowHeight);
            
            if (settings.WindowMaximized)
            {
                this.WindowState = FormWindowState.Maximized;
            }

            // Setup data binding
            lstTasks.DataSource = _filteredTasks;
            lstTasks.DisplayMember = "Title";
            lstTasks.ValueMember = "Id";

            // Setup filter combo
            cmbFilter.Items.AddRange(new[] {
                "All", "Not Started", "In Progress", "Completed", "Overdue",
                "High Priority", "Due Today", "Due This Week"
            });
            cmbFilter.SelectedItem = settings.LastSelectedFilter;

            // Setup sort combo
            cmbSort.Items.AddRange(new[] { "Title", "Due Date", "Priority", "Status", "Created Date", "Modified Date" });
            cmbSort.SelectedItem = settings.LastSortBy;

            chkSortDesc.Checked = settings.SortDescending;
            chkShowCompleted.Checked = settings.ShowCompletedTasks;

            // Setup event handlers
            cmbFilter.SelectedIndexChanged += (s, e) => ApplyFilters();
            cmbSort.SelectedIndexChanged += (s, e) => ApplyFilters();
            chkSortDesc.CheckedChanged += (s, e) => ApplyFilters();
            chkShowCompleted.CheckedChanged += (s, e) => ApplyFilters();
            lstTasks.SelectedIndexChanged += LstTasks_SelectedIndexChanged;

            // Update status
            UpdateStatusBar();
        }

        private void SetupAutoSave()
        {
            if (_settingsService.Settings.AutoSave)
            {
                _autoSaveTimer = new System.Windows.Forms.Timer();
                _autoSaveTimer.Interval = _settingsService.Settings.AutoSaveIntervalMinutes * 60 * 1000;
                _autoSaveTimer.Tick += async (s, e) => await SaveTasksAsync();
                _autoSaveTimer.Start();
            }
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await LoadTasksAsync();
            ApplyFilters();
        }

        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save current window state
            var settings = _settingsService.Settings;
            
            if (this.WindowState == FormWindowState.Normal)
            {
                settings.WindowX = this.Location.X;
                settings.WindowY = this.Location.Y;
                settings.WindowWidth = this.Size.Width;
                settings.WindowHeight = this.Size.Height;
            }
            
            settings.WindowMaximized = this.WindowState == FormWindowState.Maximized;
            settings.LastSelectedFilter = cmbFilter.SelectedItem?.ToString() ?? "All";
            settings.LastSortBy = cmbSort.SelectedItem?.ToString() ?? "CreatedDate";
            settings.SortDescending = chkSortDesc.Checked;
            settings.ShowCompletedTasks = chkShowCompleted.Checked;

            await _settingsService.SaveSettingsAsync();
            await SaveTasksAsync();

            _autoSaveTimer?.Stop();
            _autoSaveTimer?.Dispose();
        }

        private async Task LoadTasksAsync()
        {
            try
            {
                _taskCollection = await _dataService.LoadTasksAsync();
                ApplyFilters();
                UpdateStatusBar();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tasks: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task SaveTasksAsync()
        {
            try
            {
                await _dataService.SaveTasksAsync(_taskCollection);
                lblStatus.Text = $"Saved at {DateTime.Now:HH:mm:ss}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving tasks: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilters()
        {
            var filteredTasks = _taskCollection.Tasks.AsEnumerable();

            // Apply status filter
            var filterText = cmbFilter.SelectedItem?.ToString() ?? "All";
            switch (filterText)
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
            if (!chkShowCompleted.Checked)
            {
                filteredTasks = filteredTasks.Where(t => t.Status != TaskStatus.Completed);
            }

            // Apply sorting
            var sortBy = cmbSort.SelectedItem?.ToString() ?? "CreatedDate";
            var isDescending = chkSortDesc.Checked;

            filteredTasks = sortBy switch
            {
                "Title" => isDescending ? filteredTasks.OrderByDescending(t => t.Title) : filteredTasks.OrderBy(t => t.Title),
                "Due Date" => isDescending ? filteredTasks.OrderByDescending(t => t.DueDate ?? DateTime.MaxValue) : filteredTasks.OrderBy(t => t.DueDate ?? DateTime.MaxValue),
                "Priority" => isDescending ? filteredTasks.OrderByDescending(t => t.Priority) : filteredTasks.OrderBy(t => t.Priority),
                "Status" => isDescending ? filteredTasks.OrderByDescending(t => t.Status) : filteredTasks.OrderBy(t => t.Status),
                "Modified Date" => isDescending ? filteredTasks.OrderByDescending(t => t.ModifiedDate) : filteredTasks.OrderBy(t => t.ModifiedDate),
                _ => isDescending ? filteredTasks.OrderByDescending(t => t.CreatedDate) : filteredTasks.OrderBy(t => t.CreatedDate)
            };

            // Update the binding list
            _filteredTasks.Clear();
            foreach (var task in filteredTasks)
            {
                _filteredTasks.Add(task);
            }

            UpdateStatusBar();
        }

        private void UpdateStatusBar()
        {
            var totalTasks = _taskCollection.TotalTasks;
            var completedTasks = _taskCollection.CompletedTasks;
            var overdueTasks = _taskCollection.OverdueTasks;
            var completionPercentage = totalTasks > 0 ? (double)completedTasks / totalTasks * 100 : 0;

            lblTaskCount.Text = $"Tasks: {_filteredTasks.Count} of {totalTasks}";
            lblCompletedCount.Text = $"Completed: {completedTasks} ({completionPercentage:F1}%)";
            lblOverdueCount.Text = $"Overdue: {overdueTasks}";
        }

        private void LstTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedTask = lstTasks.SelectedItem as TaskItem;
            DisplayTaskDetails(selectedTask);
            
            btnEdit.Enabled = selectedTask != null;
            btnDelete.Enabled = selectedTask != null;
            btnMarkComplete.Enabled = selectedTask != null && selectedTask.Status != TaskStatus.Completed;
        }

        private void DisplayTaskDetails(TaskItem? task)
        {
            if (task == null)
            {
                lblTaskTitle.Text = "No task selected";
                lblTaskDescription.Text = "";
                lblTaskDueDate.Text = "";
                lblTaskPriority.Text = "";
                lblTaskStatus.Text = "";
                lblTaskCategories.Text = "";
                return;
            }

            lblTaskTitle.Text = task.Title;
            lblTaskDescription.Text = task.Description;
            lblTaskDueDate.Text = task.DueDate?.ToString("MMM dd, yyyy") ?? "No due date";
            lblTaskPriority.Text = task.Priority.ToString();
            lblTaskStatus.Text = task.Status.ToString();
            lblTaskCategories.Text = task.CategoriesString;
        }

        private async void btnNew_Click(object sender, EventArgs e)
        {
            using var dialog = new TaskEditForm();
            if (dialog.ShowDialog() == DialogResult.OK && dialog.Task != null)
            {
                _taskCollection.AddTask(dialog.Task);
                ApplyFilters();
                await SaveTasksAsync();
            }
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            var selectedTask = lstTasks.SelectedItem as TaskItem;
            if (selectedTask == null) return;

            using var dialog = new TaskEditForm(selectedTask);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ApplyFilters();
                await SaveTasksAsync();
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var selectedTask = lstTasks.SelectedItem as TaskItem;
            if (selectedTask == null) return;

            var result = MessageBox.Show($"Are you sure you want to delete the task '{selectedTask.Title}'?", 
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                _taskCollection.RemoveTask(selectedTask);
                ApplyFilters();
                await SaveTasksAsync();
            }
        }

        private async void btnMarkComplete_Click(object sender, EventArgs e)
        {
            var selectedTask = lstTasks.SelectedItem as TaskItem;
            if (selectedTask == null) return;

            selectedTask.Status = TaskStatus.Completed;
            ApplyFilters();
            await SaveTasksAsync();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadTasksAsync();
        }

        private async void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Export functionality will be implemented in a future version.", "Coming Soon",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Import functionality will be implemented in a future version.", "Coming Soon",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
