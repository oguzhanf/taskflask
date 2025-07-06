using TaskFlask.Models;
using TaskStatus = TaskFlask.Models.TaskStatus;

namespace TaskFlask.Forms
{
    /// <summary>
    /// Form for creating and editing tasks
    /// </summary>
    public partial class TaskEditForm : Form
    {
        private TaskItem? _task;
        private bool _isEditMode;

        public TaskItem? Task => _task;

        public TaskEditForm() : this(null)
        {
        }

        public TaskEditForm(TaskItem? task)
        {
            InitializeComponent();
            
            _task = task;
            _isEditMode = task != null;
            
            InitializeForm();
            
            if (_isEditMode && _task != null)
            {
                LoadTaskData();
            }
        }

        private void InitializeForm()
        {
            this.Text = _isEditMode ? "Edit Task" : "New Task";
            
            // Setup priority combo
            cmbPriority.Items.Clear();
            cmbPriority.Items.AddRange(Enum.GetNames(typeof(TaskPriority)));
            cmbPriority.SelectedItem = TaskPriority.Medium.ToString();
            
            // Setup status combo
            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(Enum.GetNames(typeof(TaskStatus)));
            cmbStatus.SelectedItem = TaskStatus.NotStarted.ToString();
            
            // Setup due date
            chkHasDueDate.CheckedChanged += ChkHasDueDate_CheckedChanged;
            dtpDueDate.Enabled = false;
            
            // Setup categories
            txtCategories.PlaceholderText = "Enter categories separated by commas";
        }

        private void LoadTaskData()
        {
            if (_task == null) return;
            
            txtTitle.Text = _task.Title;
            txtDescription.Text = _task.Description;
            cmbPriority.SelectedItem = _task.Priority.ToString();
            cmbStatus.SelectedItem = _task.Status.ToString();
            
            if (_task.DueDate.HasValue)
            {
                chkHasDueDate.Checked = true;
                dtpDueDate.Value = _task.DueDate.Value;
            }
            
            txtCategories.Text = string.Join(", ", _task.Categories);
        }

        private void ChkHasDueDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpDueDate.Enabled = chkHasDueDate.Checked;
            if (chkHasDueDate.Checked && dtpDueDate.Value < DateTime.Today)
            {
                dtpDueDate.Value = DateTime.Today;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;
            
            if (_task == null)
            {
                _task = new TaskItem();
            }
            
            _task.Title = txtTitle.Text.Trim();
            _task.Description = txtDescription.Text.Trim();
            _task.Priority = Enum.Parse<TaskPriority>(cmbPriority.SelectedItem?.ToString() ?? "Medium");
            _task.Status = Enum.Parse<TaskStatus>(cmbStatus.SelectedItem?.ToString() ?? "NotStarted");
            _task.DueDate = chkHasDueDate.Checked ? dtpDueDate.Value : null;
            
            // Parse categories
            var categoriesText = txtCategories.Text.Trim();
            if (!string.IsNullOrEmpty(categoriesText))
            {
                _task.Categories = categoriesText
                    .Split(',')
                    .Select(c => c.Trim())
                    .Where(c => !string.IsNullOrEmpty(c))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList();
            }
            else
            {
                _task.Categories = new List<string>();
            }
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool ValidateInput()
        {
            // Clear previous error messages
            errorProvider.Clear();
            
            bool isValid = true;
            
            // Validate title
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                errorProvider.SetError(txtTitle, "Title is required.");
                isValid = false;
            }
            
            // Validate due date
            if (chkHasDueDate.Checked && dtpDueDate.Value.Date < DateTime.Today)
            {
                errorProvider.SetError(dtpDueDate, "Due date cannot be in the past.");
                isValid = false;
            }
            
            return isValid;
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            // Clear error when user starts typing
            if (errorProvider.GetError(txtTitle) != string.Empty)
            {
                errorProvider.SetError(txtTitle, string.Empty);
            }
        }

        private void dtpDueDate_ValueChanged(object sender, EventArgs e)
        {
            // Clear error when user changes date
            if (errorProvider.GetError(dtpDueDate) != string.Empty)
            {
                errorProvider.SetError(dtpDueDate, string.Empty);
            }
        }
    }
}
