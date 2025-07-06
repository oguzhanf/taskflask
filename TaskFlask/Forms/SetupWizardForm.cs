using TaskFlask.Models;
using TaskFlask.Services;

namespace TaskFlask.Forms
{
    /// <summary>
    /// Initial setup wizard for first-time users
    /// </summary>
    public partial class SetupWizardForm : Form
    {
        private readonly CloudServiceDetector _cloudDetector;
        private List<CloudService> _detectedServices;
        private string _selectedDatabasePath = string.Empty;

        public string SelectedDatabasePath => _selectedDatabasePath;

        public SetupWizardForm()
        {
            InitializeComponent();
            _cloudDetector = new CloudServiceDetector();
            _detectedServices = new List<CloudService>();
            
            this.Load += SetupWizardForm_Load;
        }

        private async void SetupWizardForm_Load(object sender, EventArgs e)
        {
            await DetectCloudServicesAsync();
            SetupCloudServiceOptions();
        }

        private async Task DetectCloudServicesAsync()
        {
            try
            {
                lblStatus.Text = "Detecting cloud services...";
                lblStatus.Visible = true;
                Application.DoEvents();

                await Task.Run(() =>
                {
                    _detectedServices = _cloudDetector.DetectCloudServices();
                });

                lblStatus.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error detecting cloud services: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lblStatus.Visible = false;
            }
        }

        private void SetupCloudServiceOptions()
        {
            if (_detectedServices.Any())
            {
                // Show cloud service options
                var primaryService = _detectedServices.First();
                lblCloudDetected.Text = $"{primaryService.Name} detected on this machine.";
                lblCloudDetected.Visible = true;
                
                rbCloudSync.Text = $"Store database in {primaryService.Name} folder for automatic synchronization";
                rbCloudSync.Enabled = true;
                rbCloudSync.Checked = true;

                // Populate cloud service combo box
                cmbCloudServices.Items.Clear();
                foreach (var service in _detectedServices)
                {
                    cmbCloudServices.Items.Add(service);
                }
                cmbCloudServices.SelectedIndex = 0;
                cmbCloudServices.Visible = true;
                lblSelectCloud.Visible = true;
            }
            else
            {
                // No cloud services detected
                lblCloudDetected.Text = "No cloud storage services detected.";
                lblCloudDetected.Visible = true;
                rbCloudSync.Enabled = false;
                rbLocalStorage.Checked = true;
                cmbCloudServices.Visible = false;
                lblSelectCloud.Visible = false;
            }

            UpdateSelectedPath();
        }

        private void rbCloudSync_CheckedChanged(object sender, EventArgs e)
        {
            cmbCloudServices.Enabled = rbCloudSync.Checked;
            lblSelectCloud.Enabled = rbCloudSync.Checked;
            UpdateSelectedPath();
        }

        private void rbLocalStorage_CheckedChanged(object sender, EventArgs e)
        {
            btnBrowse.Enabled = rbLocalStorage.Checked;
            txtCustomPath.Enabled = rbLocalStorage.Checked;
            UpdateSelectedPath();
        }

        private void cmbCloudServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSelectedPath();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using var dialog = new SaveFileDialog
            {
                Title = "Choose location for TaskFlask database",
                Filter = "TaskFlask Database (*.xml)|*.xml|All Files (*.*)|*.*",
                DefaultExt = "xml",
                FileName = "TaskFlask.xml"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtCustomPath.Text = dialog.FileName;
                UpdateSelectedPath();
            }
        }

        private void UpdateSelectedPath()
        {
            if (rbCloudSync.Checked && cmbCloudServices.SelectedItem is CloudService service)
            {
                _selectedDatabasePath = Path.Combine(service.SyncFolderPath, "TaskFlask", "TaskFlask.xml");
            }
            else if (rbLocalStorage.Checked)
            {
                if (!string.IsNullOrEmpty(txtCustomPath.Text))
                {
                    _selectedDatabasePath = txtCustomPath.Text;
                }
                else
                {
                    var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    _selectedDatabasePath = Path.Combine(documentsPath, "TaskFlask", "TaskFlask.xml");
                }
            }

            lblSelectedPath.Text = $"Database will be stored at: {_selectedDatabasePath}";
            btnFinish.Enabled = !string.IsNullOrEmpty(_selectedDatabasePath);
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate the selected path
                var directory = Path.GetDirectoryName(_selectedDatabasePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // Test write access
                var testFile = Path.Combine(directory ?? "", "test.tmp");
                File.WriteAllText(testFile, "test");
                File.Delete(testFile);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cannot create database at the selected location: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
