namespace TaskFlask.Forms
{
    partial class TaskEditForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private TextBox txtTitle;
        private Label lblDescription;
        private TextBox txtDescription;
        private Label lblPriority;
        private ComboBox cmbPriority;
        private Label lblStatus;
        private ComboBox cmbStatus;
        private CheckBox chkHasDueDate;
        private DateTimePicker dtpDueDate;
        private Label lblCategories;
        private TextBox txtCategories;
        private Button btnOK;
        private Button btnCancel;
        private ErrorProvider errorProvider;
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
            this.components = new System.ComponentModel.Container();
            this.lblTitle = new Label();
            this.txtTitle = new TextBox();
            this.lblDescription = new Label();
            this.txtDescription = new TextBox();
            this.lblPriority = new Label();
            this.cmbPriority = new ComboBox();
            this.lblStatus = new Label();
            this.cmbStatus = new ComboBox();
            this.chkHasDueDate = new CheckBox();
            this.dtpDueDate = new DateTimePicker();
            this.lblCategories = new Label();
            this.txtCategories = new TextBox();
            this.btnOK = new Button();
            this.btnCancel = new Button();
            this.errorProvider = new ErrorProvider(this.components);
            this.pnlMain = new Panel();
            this.pnlButtons = new Panel();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(32, 15);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Title:";

            // 
            // txtTitle
            // 
            this.txtTitle.Location = new Point(20, 40);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new Size(360, 23);
            this.txtTitle.TabIndex = 1;
            this.txtTitle.TextChanged += new EventHandler(this.txtTitle_TextChanged);

            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new Point(20, 80);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new Size(70, 15);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Description:";

            // 
            // txtDescription
            // 
            this.txtDescription.Location = new Point(20, 100);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = ScrollBars.Vertical;
            this.txtDescription.Size = new Size(360, 100);
            this.txtDescription.TabIndex = 3;

            // 
            // lblPriority
            // 
            this.lblPriority.AutoSize = true;
            this.lblPriority.Location = new Point(20, 220);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new Size(48, 15);
            this.lblPriority.TabIndex = 4;
            this.lblPriority.Text = "Priority:";

            // 
            // cmbPriority
            // 
            this.cmbPriority.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbPriority.Location = new Point(80, 217);
            this.cmbPriority.Name = "cmbPriority";
            this.cmbPriority.Size = new Size(120, 23);
            this.cmbPriority.TabIndex = 5;

            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new Point(220, 220);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(42, 15);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Status:";

            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbStatus.Location = new Point(270, 217);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new Size(110, 23);
            this.cmbStatus.TabIndex = 7;

            // 
            // chkHasDueDate
            // 
            this.chkHasDueDate.AutoSize = true;
            this.chkHasDueDate.Location = new Point(20, 260);
            this.chkHasDueDate.Name = "chkHasDueDate";
            this.chkHasDueDate.Size = new Size(75, 19);
            this.chkHasDueDate.TabIndex = 8;
            this.chkHasDueDate.Text = "Due Date:";
            this.chkHasDueDate.UseVisualStyleBackColor = true;

            // 
            // dtpDueDate
            // 
            this.dtpDueDate.Location = new Point(110, 258);
            this.dtpDueDate.Name = "dtpDueDate";
            this.dtpDueDate.Size = new Size(200, 23);
            this.dtpDueDate.TabIndex = 9;
            this.dtpDueDate.ValueChanged += new EventHandler(this.dtpDueDate_ValueChanged);

            // 
            // lblCategories
            // 
            this.lblCategories.AutoSize = true;
            this.lblCategories.Location = new Point(20, 300);
            this.lblCategories.Name = "lblCategories";
            this.lblCategories.Size = new Size(66, 15);
            this.lblCategories.TabIndex = 10;
            this.lblCategories.Text = "Categories:";

            // 
            // txtCategories
            // 
            this.txtCategories.Location = new Point(20, 320);
            this.txtCategories.Name = "txtCategories";
            this.txtCategories.Size = new Size(360, 23);
            this.txtCategories.TabIndex = 11;

            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.lblTitle);
            this.pnlMain.Controls.Add(this.txtTitle);
            this.pnlMain.Controls.Add(this.lblDescription);
            this.pnlMain.Controls.Add(this.txtDescription);
            this.pnlMain.Controls.Add(this.lblPriority);
            this.pnlMain.Controls.Add(this.cmbPriority);
            this.pnlMain.Controls.Add(this.lblStatus);
            this.pnlMain.Controls.Add(this.cmbStatus);
            this.pnlMain.Controls.Add(this.chkHasDueDate);
            this.pnlMain.Controls.Add(this.dtpDueDate);
            this.pnlMain.Controls.Add(this.lblCategories);
            this.pnlMain.Controls.Add(this.txtCategories);
            this.pnlMain.Dock = DockStyle.Fill;
            this.pnlMain.Location = new Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new Size(400, 360);
            this.pnlMain.TabIndex = 12;

            // 
            // btnOK
            // 
            this.btnOK.Location = new Point(230, 10);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(75, 30);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);

            // 
            // btnCancel
            // 
            this.btnCancel.Location = new Point(315, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(75, 30);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnOK);
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Dock = DockStyle.Bottom;
            this.pnlButtons.Location = new Point(0, 360);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new Size(400, 50);
            this.pnlButtons.TabIndex = 13;

            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;

            // 
            // TaskEditForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new Size(400, 410);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlButtons);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TaskEditForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Task";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
