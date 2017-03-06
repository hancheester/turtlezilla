namespace TurtleZilla
{
    partial class IssueBrowserDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IssueBrowserDialog));
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxSearch = new System.Windows.Forms.ComboBox();
            this.listViewIssues = new System.Windows.Forms.ListView();
            this.columnId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnComponent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnPriority = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnAssignedTo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnSummary = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.labelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusWork = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.checkBoxIncludeClosed = new System.Windows.Forms.CheckBox();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxSearchField = new System.Windows.Forms.ComboBox();
            this.labelFound = new System.Windows.Forms.Label();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search:";
            // 
            // comboBoxSearch
            // 
            this.comboBoxSearch.FormattingEnabled = true;
            this.comboBoxSearch.Location = new System.Drawing.Point(62, 6);
            this.comboBoxSearch.Name = "comboBoxSearch";
            this.comboBoxSearch.Size = new System.Drawing.Size(311, 21);
            this.comboBoxSearch.TabIndex = 1;
            this.comboBoxSearch.TextChanged += new System.EventHandler(this.comboBoxSearch_TextChanged);
            // 
            // listViewIssues
            // 
            this.listViewIssues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewIssues.CheckBoxes = true;
            this.listViewIssues.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnId,
            this.columnComponent,
            this.columnStatus,
            this.columnPriority,
            this.columnAssignedTo,
            this.columnSummary});
            this.listViewIssues.FullRowSelect = true;
            this.listViewIssues.GridLines = true;
            this.listViewIssues.HideSelection = false;
            this.listViewIssues.Location = new System.Drawing.Point(12, 33);
            this.listViewIssues.MultiSelect = false;
            this.listViewIssues.Name = "listViewIssues";
            this.listViewIssues.Size = new System.Drawing.Size(822, 393);
            this.listViewIssues.TabIndex = 5;
            this.listViewIssues.UseCompatibleStateImageBehavior = false;
            this.listViewIssues.View = System.Windows.Forms.View.Details;
            this.listViewIssues.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewIssues_ItemChecked);
            // 
            // columnId
            // 
            this.columnId.Text = "ID";
            // 
            // columnComponent
            // 
            this.columnComponent.Text = "Component";
            this.columnComponent.Width = 100;
            // 
            // columnStatus
            // 
            this.columnStatus.Text = "Status";
            this.columnStatus.Width = 100;
            // 
            // columnPriority
            // 
            this.columnPriority.Text = "Priority";
            this.columnPriority.Width = 100;
            // 
            // columnAssignedTo
            // 
            this.columnAssignedTo.Text = "Assigned To";
            this.columnAssignedTo.Width = 100;
            // 
            // columnSummary
            // 
            this.columnSummary.Text = "Summary";
            this.columnSummary.Width = 1000;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelStatus,
            this.statusWork});
            this.statusStrip.Location = new System.Drawing.Point(0, 465);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(849, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 3;
            // 
            // labelStatus
            // 
            this.labelStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(834, 17);
            this.labelStatus.Spring = true;
            this.labelStatus.Text = "Ready";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusWork
            // 
            this.statusWork.AutoSize = false;
            this.statusWork.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.statusWork.Image = ((System.Drawing.Image)(resources.GetObject("statusWork.Image")));
            this.statusWork.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.statusWork.Name = "statusWork";
            this.statusWork.Size = new System.Drawing.Size(45, 17);
            this.statusWork.Text = "toolStripStatusLabel1";
            this.statusWork.Visible = false;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(762, 432);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 30);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(681, 432);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 30);
            this.buttonOK.TabIndex = 5;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // checkBoxIncludeClosed
            // 
            this.checkBoxIncludeClosed.AutoSize = true;
            this.checkBoxIncludeClosed.Location = new System.Drawing.Point(93, 440);
            this.checkBoxIncludeClosed.Name = "checkBoxIncludeClosed";
            this.checkBoxIncludeClosed.Size = new System.Drawing.Size(127, 17);
            this.checkBoxIncludeClosed.TabIndex = 6;
            this.checkBoxIncludeClosed.Text = "I&nclude closed issues";
            this.checkBoxIncludeClosed.UseVisualStyleBackColor = true;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(12, 432);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(75, 30);
            this.buttonRefresh.TabIndex = 7;
            this.buttonRefresh.Text = "&Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(379, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "in";
            // 
            // comboBoxSearchField
            // 
            this.comboBoxSearchField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSearchField.FormattingEnabled = true;
            this.comboBoxSearchField.Location = new System.Drawing.Point(400, 6);
            this.comboBoxSearchField.Name = "comboBoxSearchField";
            this.comboBoxSearchField.Size = new System.Drawing.Size(121, 21);
            this.comboBoxSearchField.TabIndex = 9;
            this.comboBoxSearchField.SelectedIndexChanged += new System.EventHandler(this.comboBoxSearchField_SelectedIndexChanged);
            // 
            // labelFound
            // 
            this.labelFound.AutoSize = true;
            this.labelFound.Location = new System.Drawing.Point(527, 9);
            this.labelFound.Name = "labelFound";
            this.labelFound.Size = new System.Drawing.Size(51, 13);
            this.labelFound.TabIndex = 10;
            this.labelFound.Text = "{0} found";
            // 
            // IssueBrowserDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 487);
            this.Controls.Add(this.labelFound);
            this.Controls.Add(this.comboBoxSearchField);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.checkBoxIncludeClosed);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.listViewIssues);
            this.Controls.Add(this.comboBoxSearch);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IssueBrowserDialog";
            this.ShowIcon = false;
            this.Text = "Issues for {0} ({1})";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxSearch;
        private System.Windows.Forms.ListView listViewIssues;
        private System.Windows.Forms.ColumnHeader columnId;
        private System.Windows.Forms.ColumnHeader columnComponent;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.ColumnHeader columnStatus;
        private System.Windows.Forms.ColumnHeader columnPriority;
        private System.Windows.Forms.ColumnHeader columnAssignedTo;
        private System.Windows.Forms.ColumnHeader columnSummary;
        private System.Windows.Forms.ToolStripStatusLabel labelStatus;
        private System.Windows.Forms.ToolStripStatusLabel statusWork;
        private System.Windows.Forms.CheckBox checkBoxIncludeClosed;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxSearchField;
        private System.Windows.Forms.Label labelFound;
    }
}