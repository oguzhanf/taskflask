namespace TaskFlask.Forms
{
    partial class SetupWizardForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblWelcome;
        private Label lblCloudDetected;
        private Label lblStatus;
        private RadioButton rbCloudSync;
        private RadioButton rbLocalStorage;
        private ComboBox cmbCloudServices;
        private Label lblSelectCloud;
        private TextBox txtCustomPath;
        private Button btnBrowse;
        private Label lblSelectedPath;
        private Button btnFinish;
        private Button btnCancel;
        private Panel pnlMain;
        private Panel pnlButtons;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblWelcome = new Label();
            this.lblCloudDetected = new Label();
            this.lblStatus = new Label();
            this.rbCloudSync = new RadioButton();
            this.rbLocalStorage = new RadioButton();
            this.cmbCloudServices = new ComboBox();
            this.lblSelectCloud = new Label();
            this.txtCustomPath = new TextBox();
            this.btnBrowse = new Button();
            this.lblSelectedPath = new Label();
            this.btnFinish = new Button();
            this.btnCancel = new Button();
            this.pnlMain = new Panel();
            this.pnlButtons = new Panel();
            this.pnlMain.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitle.Location = new Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(250, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Welcome to TaskFlask";

            // 
            // lblWelcome
            // 
            this.lblWelcome.Location = new Point(20, 60);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new Size(460, 40);
            this.lblWelcome.TabIndex = 1;
            this.lblWelcome.Text = "Let's set up your task database. Choose where you'd like to store your tasks for easy access and synchronization across devices.";

            // 
            // lblCloudDetected
            // 
            this.lblCloudDetected.Location = new Point(20, 110);
            this.lblCloudDetected.Name = "lblCloudDetected";
            this.lblCloudDetected.Size = new Size(460, 20);
            this.lblCloudDetected.TabIndex = 2;
            this.lblCloudDetected.Text = "Detecting cloud services...";
            this.lblCloudDetected.Visible = false;

            // 
            // lblStatus
            // 
            this.lblStatus.Location = new Point(20, 110);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(460, 20);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Detecting cloud services...";
            this.lblStatus.Visible = false;

            // 
            // rbCloudSync
            // 
            this.rbCloudSync.Location = new Point(40, 140);
            this.rbCloudSync.Name = "rbCloudSync";
            this.rbCloudSync.Size = new Size(440, 20);
            this.rbCloudSync.TabIndex = 4;
            this.rbCloudSync.Text = "Store database in cloud folder for automatic synchronization";
            this.rbCloudSync.UseVisualStyleBackColor = true;
            this.rbCloudSync.CheckedChanged += new EventHandler(this.rbCloudSync_CheckedChanged);

            // 
            // cmbCloudServices
            // 
            this.cmbCloudServices.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCloudServices.Location = new Point(60, 190);
            this.cmbCloudServices.Name = "cmbCloudServices";
            this.cmbCloudServices.Size = new Size(300, 23);
            this.cmbCloudServices.TabIndex = 5;
            this.cmbCloudServices.Visible = false;
            this.cmbCloudServices.SelectedIndexChanged += new EventHandler(this.cmbCloudServices_SelectedIndexChanged);

            // 
            // lblSelectCloud
            // 
            this.lblSelectCloud.Location = new Point(60, 170);
            this.lblSelectCloud.Name = "lblSelectCloud";
            this.lblSelectCloud.Size = new Size(200, 15);
            this.lblSelectCloud.TabIndex = 6;
            this.lblSelectCloud.Text = "Select cloud service:";
            this.lblSelectCloud.Visible = false;

            // 
            // rbLocalStorage
            // 
            this.rbLocalStorage.Location = new Point(40, 230);
            this.rbLocalStorage.Name = "rbLocalStorage";
            this.rbLocalStorage.Size = new Size(440, 20);
            this.rbLocalStorage.TabIndex = 7;
            this.rbLocalStorage.Text = "Store database locally (choose custom location)";
            this.rbLocalStorage.UseVisualStyleBackColor = true;
            this.rbLocalStorage.CheckedChanged += new EventHandler(this.rbLocalStorage_CheckedChanged);

            // 
            // txtCustomPath
            // 
            this.txtCustomPath.Location = new Point(60, 260);
            this.txtCustomPath.Name = "txtCustomPath";
            this.txtCustomPath.Size = new Size(300, 23);
            this.txtCustomPath.TabIndex = 8;
            this.txtCustomPath.Enabled = false;

            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new Point(370, 260);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new Size(75, 23);
            this.btnBrowse.TabIndex = 9;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Enabled = false;
            this.btnBrowse.Click += new EventHandler(this.btnBrowse_Click);

            // 
            // lblSelectedPath
            // 
            this.lblSelectedPath.Location = new Point(20, 310);
            this.lblSelectedPath.Name = "lblSelectedPath";
            this.lblSelectedPath.Size = new Size(460, 40);
            this.lblSelectedPath.TabIndex = 10;
            this.lblSelectedPath.Text = "Database will be stored at: ";

            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.lblTitle);
            this.pnlMain.Controls.Add(this.lblWelcome);
            this.pnlMain.Controls.Add(this.lblCloudDetected);
            this.pnlMain.Controls.Add(this.lblStatus);
            this.pnlMain.Controls.Add(this.rbCloudSync);
            this.pnlMain.Controls.Add(this.cmbCloudServices);
            this.pnlMain.Controls.Add(this.lblSelectCloud);
            this.pnlMain.Controls.Add(this.rbLocalStorage);
            this.pnlMain.Controls.Add(this.txtCustomPath);
            this.pnlMain.Controls.Add(this.btnBrowse);
            this.pnlMain.Controls.Add(this.lblSelectedPath);
            this.pnlMain.Dock = DockStyle.Fill;
            this.pnlMain.Location = new Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new Size(500, 360);
            this.pnlMain.TabIndex = 11;

            // 
            // btnFinish
            // 
            this.btnFinish.Location = new Point(330, 10);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new Size(75, 30);
            this.btnFinish.TabIndex = 0;
            this.btnFinish.Text = "Finish";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Enabled = false;
            this.btnFinish.Click += new EventHandler(this.btnFinish_Click);

            // 
            // btnCancel
            // 
            this.btnCancel.Location = new Point(415, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(75, 30);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnFinish);
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Dock = DockStyle.Bottom;
            this.pnlButtons.Location = new Point(0, 360);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new Size(500, 50);
            this.pnlButtons.TabIndex = 12;

            // 
            // SetupWizardForm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(500, 410);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlButtons);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupWizardForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "TaskFlask Setup";
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
