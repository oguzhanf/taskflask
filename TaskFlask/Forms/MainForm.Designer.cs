namespace TaskFlask.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private MenuStrip menuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newTaskToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem exportToolStripMenuItem;
        private ToolStripMenuItem importToolStripMenuItem;
        private Panel pnlLeft;
        private Panel pnlRight;
        private Splitter splitter;
        private GroupBox grpFilters;
        private Label lblFilter;
        private ComboBox cmbFilter;
        private Label lblSort;
        private ComboBox cmbSort;
        private CheckBox chkSortDesc;
        private CheckBox chkShowCompleted;
        private ListBox lstTasks;
        private Panel pnlButtons;
        private Button btnNew;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnMarkComplete;
        private Button btnRefresh;
        private GroupBox grpTaskDetails;
        private Label lblTaskTitle;
        private Label lblTaskDescription;
        private Label lblTaskDueDate;
        private Label lblTaskPriority;
        private Label lblTaskStatus;
        private Label lblTaskCategories;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblTaskCount;
        private ToolStripStatusLabel lblCompletedCount;
        private ToolStripStatusLabel lblOverdueCount;
        private ToolStripStatusLabel lblStatus;

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
            this.menuStrip = new MenuStrip();
            this.fileToolStripMenuItem = new ToolStripMenuItem();
            this.newTaskToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.exportToolStripMenuItem = new ToolStripMenuItem();
            this.importToolStripMenuItem = new ToolStripMenuItem();
            this.exitToolStripMenuItem = new ToolStripMenuItem();
            this.pnlLeft = new Panel();
            this.pnlRight = new Panel();
            this.splitter = new Splitter();
            this.grpFilters = new GroupBox();
            this.lblFilter = new Label();
            this.cmbFilter = new ComboBox();
            this.lblSort = new Label();
            this.cmbSort = new ComboBox();
            this.chkSortDesc = new CheckBox();
            this.chkShowCompleted = new CheckBox();
            this.lstTasks = new ListBox();
            this.pnlButtons = new Panel();
            this.btnNew = new Button();
            this.btnEdit = new Button();
            this.btnDelete = new Button();
            this.btnMarkComplete = new Button();
            this.btnRefresh = new Button();
            this.grpTaskDetails = new GroupBox();
            this.lblTaskTitle = new Label();
            this.lblTaskDescription = new Label();
            this.lblTaskDueDate = new Label();
            this.lblTaskPriority = new Label();
            this.lblTaskStatus = new Label();
            this.lblTaskCategories = new Label();
            this.statusStrip = new StatusStrip();
            this.lblTaskCount = new ToolStripStatusLabel();
            this.lblCompletedCount = new ToolStripStatusLabel();
            this.lblOverdueCount = new ToolStripStatusLabel();
            this.lblStatus = new ToolStripStatusLabel();
            this.menuStrip.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.grpFilters.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.grpTaskDetails.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();

            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new ToolStripItem[] { this.fileToolStripMenuItem });
            this.menuStrip.Location = new Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new Size(1000, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";

            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                this.newTaskToolStripMenuItem,
                this.toolStripSeparator1,
                this.exportToolStripMenuItem,
                this.importToolStripMenuItem,
                this.exitToolStripMenuItem
            });
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";

            // 
            // newTaskToolStripMenuItem
            // 
            this.newTaskToolStripMenuItem.Name = "newTaskToolStripMenuItem";
            this.newTaskToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            this.newTaskToolStripMenuItem.Size = new Size(180, 22);
            this.newTaskToolStripMenuItem.Text = "&New Task";
            this.newTaskToolStripMenuItem.Click += new EventHandler(this.btnNew_Click);

            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new Size(177, 6);

            //
            // exportToolStripMenuItem
            //
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new Size(180, 22);
            this.exportToolStripMenuItem.Text = "&Export...";
            this.exportToolStripMenuItem.Click += new EventHandler(this.exportToolStripMenuItem_Click);

            //
            // importToolStripMenuItem
            //
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new Size(180, 22);
            this.importToolStripMenuItem.Text = "&Import...";
            this.importToolStripMenuItem.Click += new EventHandler(this.importToolStripMenuItem_Click);

            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new Size(180, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new EventHandler((s, e) => this.Close());

            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.lstTasks);
            this.pnlLeft.Controls.Add(this.grpFilters);
            this.pnlLeft.Controls.Add(this.pnlButtons);
            this.pnlLeft.Dock = DockStyle.Left;
            this.pnlLeft.Location = new Point(0, 24);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new Size(350, 626);
            this.pnlLeft.TabIndex = 1;

            // 
            // splitter
            // 
            this.splitter.Location = new Point(350, 24);
            this.splitter.Name = "splitter";
            this.splitter.Size = new Size(3, 626);
            this.splitter.TabIndex = 2;
            this.splitter.TabStop = false;

            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.grpTaskDetails);
            this.pnlRight.Dock = DockStyle.Fill;
            this.pnlRight.Location = new Point(353, 24);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new Size(647, 626);
            this.pnlRight.TabIndex = 3;

            // 
            // grpFilters
            // 
            this.grpFilters.Controls.Add(this.lblFilter);
            this.grpFilters.Controls.Add(this.cmbFilter);
            this.grpFilters.Controls.Add(this.lblSort);
            this.grpFilters.Controls.Add(this.cmbSort);
            this.grpFilters.Controls.Add(this.chkSortDesc);
            this.grpFilters.Controls.Add(this.chkShowCompleted);
            this.grpFilters.Dock = DockStyle.Top;
            this.grpFilters.Location = new Point(0, 0);
            this.grpFilters.Name = "grpFilters";
            this.grpFilters.Size = new Size(350, 120);
            this.grpFilters.TabIndex = 0;
            this.grpFilters.TabStop = false;
            this.grpFilters.Text = "Filters && Sorting";

            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Location = new Point(10, 25);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new Size(36, 15);
            this.lblFilter.TabIndex = 0;
            this.lblFilter.Text = "Filter:";

            // 
            // cmbFilter
            // 
            this.cmbFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbFilter.Location = new Point(60, 22);
            this.cmbFilter.Name = "cmbFilter";
            this.cmbFilter.Size = new Size(120, 23);
            this.cmbFilter.TabIndex = 1;

            // 
            // lblSort
            // 
            this.lblSort.AutoSize = true;
            this.lblSort.Location = new Point(190, 25);
            this.lblSort.Name = "lblSort";
            this.lblSort.Size = new Size(32, 15);
            this.lblSort.TabIndex = 2;
            this.lblSort.Text = "Sort:";

            // 
            // cmbSort
            // 
            this.cmbSort.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbSort.Location = new Point(230, 22);
            this.cmbSort.Name = "cmbSort";
            this.cmbSort.Size = new Size(110, 23);
            this.cmbSort.TabIndex = 3;

            // 
            // chkSortDesc
            // 
            this.chkSortDesc.AutoSize = true;
            this.chkSortDesc.Location = new Point(10, 55);
            this.chkSortDesc.Name = "chkSortDesc";
            this.chkSortDesc.Size = new Size(86, 19);
            this.chkSortDesc.TabIndex = 4;
            this.chkSortDesc.Text = "Descending";
            this.chkSortDesc.UseVisualStyleBackColor = true;

            // 
            // chkShowCompleted
            // 
            this.chkShowCompleted.AutoSize = true;
            this.chkShowCompleted.Location = new Point(10, 85);
            this.chkShowCompleted.Name = "chkShowCompleted";
            this.chkShowCompleted.Size = new Size(118, 19);
            this.chkShowCompleted.TabIndex = 5;
            this.chkShowCompleted.Text = "Show Completed";
            this.chkShowCompleted.UseVisualStyleBackColor = true;

            // 
            // lstTasks
            // 
            this.lstTasks.Dock = DockStyle.Fill;
            this.lstTasks.Location = new Point(0, 120);
            this.lstTasks.Name = "lstTasks";
            this.lstTasks.Size = new Size(350, 456);
            this.lstTasks.TabIndex = 1;

            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnNew);
            this.pnlButtons.Controls.Add(this.btnEdit);
            this.pnlButtons.Controls.Add(this.btnDelete);
            this.pnlButtons.Controls.Add(this.btnMarkComplete);
            this.pnlButtons.Controls.Add(this.btnRefresh);
            this.pnlButtons.Dock = DockStyle.Bottom;
            this.pnlButtons.Location = new Point(0, 576);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new Size(350, 50);
            this.pnlButtons.TabIndex = 2;

            // 
            // btnNew
            // 
            this.btnNew.Location = new Point(10, 10);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new Size(60, 30);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new EventHandler(this.btnNew_Click);

            // 
            // btnEdit
            // 
            this.btnEdit.Location = new Point(80, 10);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new Size(60, 30);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Enabled = false;
            this.btnEdit.Click += new EventHandler(this.btnEdit_Click);

            // 
            // btnDelete
            // 
            this.btnDelete.Location = new Point(150, 10);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(60, 30);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Enabled = false;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);

            // 
            // btnMarkComplete
            // 
            this.btnMarkComplete.Location = new Point(220, 10);
            this.btnMarkComplete.Name = "btnMarkComplete";
            this.btnMarkComplete.Size = new Size(60, 30);
            this.btnMarkComplete.TabIndex = 3;
            this.btnMarkComplete.Text = "Done";
            this.btnMarkComplete.UseVisualStyleBackColor = true;
            this.btnMarkComplete.Enabled = false;
            this.btnMarkComplete.Click += new EventHandler(this.btnMarkComplete_Click);

            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new Point(290, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new Size(50, 30);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "↻";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);

            // 
            // grpTaskDetails
            // 
            this.grpTaskDetails.Controls.Add(this.lblTaskTitle);
            this.grpTaskDetails.Controls.Add(this.lblTaskDescription);
            this.grpTaskDetails.Controls.Add(this.lblTaskDueDate);
            this.grpTaskDetails.Controls.Add(this.lblTaskPriority);
            this.grpTaskDetails.Controls.Add(this.lblTaskStatus);
            this.grpTaskDetails.Controls.Add(this.lblTaskCategories);
            this.grpTaskDetails.Dock = DockStyle.Fill;
            this.grpTaskDetails.Location = new Point(0, 0);
            this.grpTaskDetails.Name = "grpTaskDetails";
            this.grpTaskDetails.Size = new Size(647, 626);
            this.grpTaskDetails.TabIndex = 0;
            this.grpTaskDetails.TabStop = false;
            this.grpTaskDetails.Text = "Task Details";

            // 
            // lblTaskTitle
            // 
            this.lblTaskTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTaskTitle.Location = new Point(20, 30);
            this.lblTaskTitle.Name = "lblTaskTitle";
            this.lblTaskTitle.Size = new Size(600, 30);
            this.lblTaskTitle.TabIndex = 0;
            this.lblTaskTitle.Text = "No task selected";

            // 
            // lblTaskDescription
            // 
            this.lblTaskDescription.Location = new Point(20, 70);
            this.lblTaskDescription.Name = "lblTaskDescription";
            this.lblTaskDescription.Size = new Size(600, 200);
            this.lblTaskDescription.TabIndex = 1;

            // 
            // lblTaskDueDate
            // 
            this.lblTaskDueDate.Location = new Point(20, 280);
            this.lblTaskDueDate.Name = "lblTaskDueDate";
            this.lblTaskDueDate.Size = new Size(200, 20);
            this.lblTaskDueDate.TabIndex = 2;

            // 
            // lblTaskPriority
            // 
            this.lblTaskPriority.Location = new Point(20, 310);
            this.lblTaskPriority.Name = "lblTaskPriority";
            this.lblTaskPriority.Size = new Size(200, 20);
            this.lblTaskPriority.TabIndex = 3;

            // 
            // lblTaskStatus
            // 
            this.lblTaskStatus.Location = new Point(20, 340);
            this.lblTaskStatus.Name = "lblTaskStatus";
            this.lblTaskStatus.Size = new Size(200, 20);
            this.lblTaskStatus.TabIndex = 4;

            // 
            // lblTaskCategories
            // 
            this.lblTaskCategories.Location = new Point(20, 370);
            this.lblTaskCategories.Name = "lblTaskCategories";
            this.lblTaskCategories.Size = new Size(600, 20);
            this.lblTaskCategories.TabIndex = 5;

            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new ToolStripItem[] {
                this.lblTaskCount,
                this.lblCompletedCount,
                this.lblOverdueCount,
                this.lblStatus
            });
            this.statusStrip.Location = new Point(0, 650);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new Size(1000, 22);
            this.statusStrip.TabIndex = 4;
            this.statusStrip.Text = "statusStrip";

            // 
            // lblTaskCount
            // 
            this.lblTaskCount.Name = "lblTaskCount";
            this.lblTaskCount.Size = new Size(70, 17);
            this.lblTaskCount.Text = "Tasks: 0 of 0";

            // 
            // lblCompletedCount
            // 
            this.lblCompletedCount.Name = "lblCompletedCount";
            this.lblCompletedCount.Size = new Size(78, 17);
            this.lblCompletedCount.Text = "Completed: 0";

            // 
            // lblOverdueCount
            // 
            this.lblOverdueCount.Name = "lblOverdueCount";
            this.lblOverdueCount.Size = new Size(65, 17);
            this.lblOverdueCount.Text = "Overdue: 0";

            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(39, 17);
            this.lblStatus.Text = "Ready";
            this.lblStatus.Spring = true;
            this.lblStatus.TextAlign = ContentAlignment.MiddleRight;

            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1000, 672);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.splitter);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "TaskFlask - Task Management";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.grpFilters.ResumeLayout(false);
            this.grpFilters.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.grpTaskDetails.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
